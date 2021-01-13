using System;
using System.Collections.Generic;

using SmallBaseDevKit.Main;
using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Базовый класс игровой единицы, которая хранит в себе ряд актуальных состояний (<see cref="BaseUnitState{T}"/>) и Unity компонентов.
    /// </summary>
    public abstract class BaseUnit : IUpdtable, IUnit
    {
        private LinkedList<IState> _unitStateList;

        protected BaseUnit()
        {
            _Debug.Log($"Create new unit - {this.GetType().Name}", DebugColor.blue);
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
        /// Расширенный метод настройки потомков класса.
        /// </summary>
        protected abstract void ExtendedSetupUnit();
        #endregion

        #region IUnit Method
        void IUnit.CreateUnit()
        {
            //TODO добавить данные настройки
            ExtendedSetupUnit();
        }

        void IUnit.AddUnitState<T>()
        {
            if (!GameUtiles.ContainObjectInLinkedList(_unitStateList, typeof(T), EqualState))
            {
                var state = GameInstance.Instance.GetGameModule<UnitStateModule>().GetState<T>();
                _unitStateList.AddFirst(state);
                _Debug.Log($"Add new unit state - {state.ToString()}", DebugColor.green);
            }
        }

        void IUnit.RemoveState(IState state)
        {
            _unitStateList.Remove(state);
            state.StateRemove();
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

        IState IUnit.GetUnitState<S>()
        {
            GameUtiles.TryGetObjectInLinkedList(_unitStateList, typeof(S), out var state, EqualState);
            return state;
        }

        UnityEngine.Object IUnit.GetUnitComponent<C>()
        {
            throw new NotImplementedException();
        }

        void IUnit.UpdateUnitState()
        {
            var node = _unitStateList.First;
            for(int i = 0; i < _unitStateList.Count; ++i)
            {
                node.Value.Execute();
                if (node.Value.CheckCompliteState())
                {
                    var deleteNode = new LinkedListNode<IState>(node.Value);
                    deleteNode.Value.StateRemove();
                    _unitStateList.Remove(deleteNode);
                }
                node = node.Next;
            }
        }
        #endregion

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
