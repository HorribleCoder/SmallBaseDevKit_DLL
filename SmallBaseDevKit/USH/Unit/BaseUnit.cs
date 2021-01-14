using System;
using System.Collections.Generic;
using UnityEngine;

using SmallBaseDevKit.Main;
using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.State;


namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Базовый класс игровой единицы, которая хранит в себе ряд актуальных состояний (<see cref="BaseUnitState{T}"/>).
    /// </summary>
    /// <typeparam name="UnitData">Входные данные настройки юнита, формат - <see cref="ScriptableObject"/>.</typeparam>
    public abstract class BaseUnit<UnitData> : IUpdtable, IUnit
        where UnitData: ScriptableObject
    {
        /// <summary>
        /// Слой с Unity компонентами что использует игровая единица. Необхоидмо произвести настройку в потомках BaseUnit. <see cref="ComponentHandler.SetupComponentHandler{T}(T, GameObject)"/>
        /// </summary>
        protected ComponentHandler componentHandler;
        private LinkedList<IState> _unitStateList;
        private UnitData unitData;


        protected BaseUnit()
        {
            _Debug.Log($"Create new unit - {this.GetType().Name}", DebugColor.blue);
            componentHandler = new ComponentHandler();
            _unitStateList = new LinkedList<IState>();
            GameUpdateHandler.Instance.Registration(this);
        }

        public virtual void OnFixedUpdate()
        {
            //do something...
        }

        public virtual void OnUpdate()
        {
            //do something...
        }

        #region Abstract Methods
        /// <summary>
        /// Расширенный метод расширенной настройки для потомков.
        /// </summary>
        protected abstract void ExtendedSetupUnit(UnitData unitData);
        #endregion

        #region IUnit Method
        void IUnit.CreateUnit<Data>(Data unitData)
        {
            this.unitData = ConvertInputData(unitData);
            ExtendedSetupUnit(this.unitData);
        }

        ReadData IUnit.ReadUnitData<ReadData>()
        {
            return unitData as ReadData;
        }

        void IUnit.AddUnitState<T>()
        {
            if (!GameUtiles.ContainObjectInLinkedList(_unitStateList, typeof(T), EqualState))
            {
                var state = GameInstance.Instance.GetGameModule<UnitStateModule>().GetState<T>();
                state.SetupState(this);
                _unitStateList.AddFirst(state);
                _Debug.Log($"Add new unit state - {state}", DebugColor.green);
            }
        }
        void IUnit.RemoveState<RemoveState>()
        {
            if(GameUtiles.TryGetObjectInLinkedList(_unitStateList, typeof(RemoveState), out var findState, EqualState))
            {
                _unitStateList.Remove(findState);
                findState.StateRemove();
            }
        }

        void IUnit.DestroyUnit()
        {
            var node = _unitStateList.First;
            for(int i = 0; i < _unitStateList.Count; ++i)
            {

                node.Value.StateRemove();
                node = node.Next;
            }
            _unitStateList.Clear();
            GameInstance.Instance.GetGameModule<UnitModule>().ReturnUnit(this);
        }

        S IUnit.GetUnitState<S>()
        {
            GameUtiles.TryGetObjectInLinkedList(_unitStateList, typeof(S), out var state, EqualState);
            return (S)state;
        }

        bool IUnit.TryGetUnitComponent<C>(out C component)
        {
            component = componentHandler.GetComponent<C>();
            return component != default;
        }

        void IUnit.UpdateUnitState()
        {
            var node = _unitStateList.First;
            for(int i = 0; i < _unitStateList.Count; ++i)
            {
                node.Value.Execute();
                if (node.Value.CheckCompliteState())
                {
                    node.Value.StateRemove();
                    _unitStateList.Remove(node);
                }
                node = node.Next;
            }
        }
        #endregion

        protected UnitData ConvertInputData(ScriptableObject unitData)
        {
            return (UnitData)unitData;
        }


        private bool EqualState(object pivotObject, object equalObject)
        {
            var result = false;
            if(pivotObject is Type)
            {
                result = (Type)pivotObject == equalObject.GetType();
            }
            else
            {
                result = pivotObject.GetType() == equalObject.GetType();
            }
            return result;
        }
    }
}
