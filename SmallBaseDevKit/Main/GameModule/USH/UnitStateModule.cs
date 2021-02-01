using System;
using System.Collections.Generic;
using SmallBaseDevKit.Pool;
using SmallBaseDevKit.USH.State;
using SmallBaseDevKit.USH.Unit;
using SmallBaseDevKit.Main;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitStateModule : BaseGameModule
    {
        private IPool<IState> _statePool;

        private IDictionary<IUnit, Queue<IState>> _firstAddStateAwaitList;
        private IDictionary<IUnit, Queue<IState>> _lastAddStateAwaitList;

        protected override void CreateModule()
        {
            _statePool = new TablePool<IState>();

            _firstAddStateAwaitList = new Dictionary<IUnit, Queue<IState>>();
            _lastAddStateAwaitList = new Dictionary<IUnit, Queue<IState>>();

            GameUpdateHandler.Instance.invokeLateUpdateSubprocessEvent += ExecuteLateAddState;
        }

        public T GetState<T>() where T : IState
        {
            return (T)_statePool.GetObject(typeof(T));
        }

        public void ReturnState(IState state)
        {
            _statePool.ReturnObject(state);
        }

        public override void DebugModule()
        {
            base.DebugModule();
            _statePool.PoolDebugView();
        }

        internal void AddStateInAwaitList(IUnit unit, IState state, AddStateType addStateType)
        {
            var currentDictionary = (addStateType == AddStateType.AddFirst) ? _firstAddStateAwaitList : _lastAddStateAwaitList;
            if(!currentDictionary.TryGetValue(unit, out var currentQueue))
            {
                currentQueue = new Queue<IState>();
                currentDictionary.Add(unit, currentQueue);
            }
            currentQueue.Enqueue(state);
        }

        internal void ExecuteLateAddState()
        {
            AddStateProcess(_firstAddStateAwaitList, AddStateType.AddFirst);
            AddStateProcess(_lastAddStateAwaitList, AddStateType.AddLast);
        }

        private void AddStateProcess(IDictionary<IUnit, Queue<IState>> currentDictionary, AddStateType addStateType)
        {
            if (currentDictionary.Count <= 0) return;
            foreach(var element in currentDictionary)
            {
                while (element.Value.Count > 0)
                {
                    element.Key.AddUnitState(element.Value.Dequeue(), addStateType);
                }
            }
            currentDictionary.Clear();
        }
    }
}
