using System;
using System.Collections.Generic;
using System.Text;

using SmallBaseDevKit.Main;
using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.State;
using UnityEngine;

namespace SmallBaseDevKit.USH.Unit
{
    public abstract class BaseUnit : IUpdatable, IUnit, IUnitVisual
    {
        /// <summary>
        /// Слой с Unity компонентами что использует игровая единица. Необходимо произвести настройку в потомках BaseUnit, через вызов метода - SetupComponentHandler
        /// </summary>
        protected ComponentHandler ComponentHandler
        {
            get
            {
                if (_componentHandler is null)
                {
                    _componentHandler = new ComponentHandler();
                }
                return _componentHandler;
            }
        }
        private ComponentHandler _componentHandler;
        private LinkedList<IState> _unitStateList;
        public bool isActive => _isActive;

        private bool _isActive;

        #region Abstract Methods
        /// <summary>
        /// Метод расширенной настройки для потомков.
        /// </summary>
        protected abstract void ExtendedSetupUnit();
        /// <summary>
        /// Метод расширенного уничтожения для потомков.
        /// <para>Вызывается до очистки списка состояний и возврата в единицы в пул.</para>
        /// </summary>
        protected abstract void ExtendedDestroyUnit();
        #endregion

        protected BaseUnit()
        {
            _unitStateList = new LinkedList<IState>();
        }

        #region IUpdatable Methods
        void IUpdatable.OnUpdate()
        {
            OnUpdate();
        }
        void IUpdatable.OnFixedUpdate()
        {
            OnFixedUpdate();
        }
          
        public virtual void OnFixedUpdate()
        {
            //do something...
        }

        public virtual void OnUpdate()
        {
            //do something...
        }
        #endregion


        #region IUnit Method
        void IUnit.CreateUnit()
        {
            GameUpdateHandler.Instance.Registration(this);
            ExtendedSetupUnit();
            _isActive = true;
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
            if (GameUtiles.TryGetObjectInLinkedList(_unitStateList, typeof(RemoveState), out var findState, EqualState))
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
            for (int i = 0; i < _unitStateList.Count; ++i)
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
            component = ComponentHandler.GetComponent<C>();
            return component != default;
        }

        #endregion
        private bool EqualState(object pivotObject, object equalObject)
        {
            var result = false;
            if (pivotObject is Type)
            {
                result = (Type)pivotObject == equalObject.GetType();
            }
            else
            {
                result = pivotObject.GetType() == equalObject.GetType();
            }
            return result;
        }

        void IUnitVisual.AddUnitVisual(GameObject visualPrototype)
        {
            if(ComponentHandler.UnitVisual != null)
            {
                this.RemoveUnitVisual();
            }
            var unitVisual = Game.GetGameModule<UnitVisualModule>().GetUnitVisualByPrototype(visualPrototype);
            ComponentHandler.SetupComponentHandler(this, unitVisual);
        }

        void IUnitVisual.RemoveUnitVisual()
        {
            if(ComponentHandler.UnitVisual != null)
            {
                Game.GetGameModule<UnitVisualModule>().ReturnUnitVisual(ComponentHandler.UnitVisual);
            }
            ComponentHandler.ResetComponentHandler();
        }
        public void DebugViewUnit()
        {
            _Debug.Log(this.GetType(), DebugColor.blue);
            ComponentHandler.DebugView();
        }
    }
}
