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

        private UnitData _data;

        public bool isActive => _isActive;
        private bool _isActive;

        protected BaseUnit()
        {
            componentHandler = new ComponentHandler();
            _unitStateList = new LinkedList<IState>();
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
        /// Метод расширенной настройки для потомков.
        /// <para>Вызывается после получения основных данных игровой единицы в момет создания экземпляра класса.</para>
        /// </summary>
        protected abstract void ExtendedSetupUnit(UnitData unitData);
        /// <summary>
        /// Метод расширенного уничтожения для потомков.
        /// <para>Вызывается до очистки списка состояний и возврата в единицы в пул.</para>
        /// </summary>
        protected abstract void ExtendedDestroyUnit();
        #endregion

        #region IUnit Method
        void IUnit.CreateUnit<Data>(Data unitData)
        {
            this._data = ConvertInputData(unitData);
            GameUpdateHandler.Instance.Registration(this);
            ExtendedSetupUnit(this._data);
            _isActive = true;
        }

        ReadData IUnit.ReadUnitData<ReadData>()
        {
            return _data as ReadData;
        }

        void IUnit.AddUnitState(IState state, AddStateType addStateType)
        {
            state.SetupState(this);
            if (addStateType == AddStateType.AddFirst)
            {
                _unitStateList.AddFirst(state);
            }
            else
            {
                _unitStateList.AddLast(state);
            }
        }
        void IUnit.UpdateUnitState()
        {
            var node = _unitStateList.First;
            for (int i = 0; i < _unitStateList.Count; ++i)
            {
                node.Value.Execute();

                if (node.Value.CheckCompliteState())
                {
                    var nextNode = node.Next;
                    node.Value.StateRemove();
                    _unitStateList.Remove(node);
                    node = nextNode;
                }
                else
                {
                    node = node.Next;
                }
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
            GameUpdateHandler.Instance.Unregistration(this);
            ExtendedDestroyUnit();
            var node = _unitStateList.First;
            for(int i = 0; i < _unitStateList.Count; ++i)
            {

                node.Value.StateRemove();
                node = node.Next;
            }
            _unitStateList.Clear();
            GameInstance.Instance.GetGameModule<UnitModule>().ReturnUnit(this);
            _isActive = false;
        }

        bool IUnit.TryGetUnitState<S>(out S state)
        {
            var result = GameUtiles.TryGetObjectInLinkedList(_unitStateList, typeof(S), out var findState, EqualState);
            state = (S)findState;
            return result;
        }

        bool IUnit.TryGetUnitComponent<C>(out C component)
        {
            component = componentHandler.GetComponent<C>();
            return component != default;
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
