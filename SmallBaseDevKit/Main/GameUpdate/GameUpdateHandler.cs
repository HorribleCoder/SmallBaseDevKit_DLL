using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using SmallBaseDevKit.USH.Unit;


namespace SmallBaseDevKit.Main
{
    public sealed class GameUpdateHandler : MonoSingleton<GameUpdateHandler>, IRegistrator<IUpdatable>
    {
        private LinkedList<IUpdatable> _updatableList;
        private IList<IUpdatable> _removableList;

        internal delegate void InvokeLateUpdateSubprocess();
        internal event InvokeLateUpdateSubprocess invokeLateUpdateSubprocessEvent;

        public void Registration(IUpdatable obj)
        {
            if(_updatableList is null)
            {
                _updatableList = new LinkedList<IUpdatable>();
            }
            if (!_updatableList.Contains(obj))
            {
                _updatableList.AddLast(obj);
            }
        }

        public void Unregistration(IUpdatable obj)
        {
            if (_updatableList is null) return;
            if(_removableList is null)
            {
                _removableList = new List<IUpdatable>();
            }
            _removableList.Add(obj);
        }

        private void Start()
        {
            StartCoroutine(LateAddStateForUnitSubprocess());
        }

        private void Update()
        {
            //Производим обновление игровых единиц + нвсегда обновляем актуальные состояния и компоненты в игровой единице.
            var node = _updatableList.First;
            IUnit unitNode = default;
            while(node != null)
            {
                if (node.Value is IUnit)
                {
                    unitNode = (IUnit)node.Value;
                    unitNode.UpdateUnitState();
                }
                node.Value.OnUpdate();
                node = node.Next;
            }
        }

        private void FixedUpdate()
        {
            var node = _updatableList.First;
            for (int i = 0; i < _updatableList.Count; ++i)
            {
                node.Value.OnFixedUpdate();
                node = node.Next;
            }
        }

        private void LateUpdate()
        {
            if (_removableList is null || _removableList.Count == 0) return;
            for(int i = 0; i < _removableList.Count; ++i)
            {
                _updatableList.Remove(_removableList[i]);
            }
            _removableList.Clear();
        }

        private IEnumerator LateAddStateForUnitSubprocess()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                invokeLateUpdateSubprocessEvent?.Invoke();
            }
        }
    }
}
