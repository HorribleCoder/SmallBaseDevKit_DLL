using System.Collections.Generic;

using UnityEngine;
using SmallBaseDevKit.USH.Unit;


namespace SmallBaseDevKit.Main
{
    public sealed class GameUpdateHandler : MonoSingleton<GameUpdateHandler>, IRegistrator<IUpdtable>
    {
        private LinkedList<IUpdtable> _updatableList;

        public void Registration(IUpdtable obj)
        {
            if(_updatableList is null)
            {
                _updatableList = new LinkedList<IUpdtable>();
            }
            if (!_updatableList.Contains(obj))
            {
                _updatableList.AddLast(obj);
            }
        }

        public void Unregistration(IUpdtable obj)
        {
            if (_updatableList is null) return;
            if (_updatableList.Contains(obj))
            {
                _updatableList.Remove(obj);
            }
        }

        private void Update()
        {
            //Производим обновление игровых единиц + нвсегда обновляем актуальные состояния и компоненты в игровой единице.
            var node = _updatableList.First;
            IUnit unitNode = default;
            for(int i = 0; i < _updatableList.Count; ++i)
            {
                if(node.Value is IUnit)
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
            for(int i = 0; i < _updatableList.Count; ++i)
            {
                node.Value.OnFixedUpdate();
                node = node.Next;
            }
        }
    }
}
