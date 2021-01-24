﻿using System.Collections.Generic;

using UnityEngine;
using SmallBaseDevKit.USH.Unit;


namespace SmallBaseDevKit.Main
{
    public sealed class GameUpdateHandler : MonoSingleton<GameUpdateHandler>, IRegistrator<IUpdtable>
    {
        private LinkedList<IUpdtable> _updatableList;
        private IList<IUpdtable> _removableList;

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
            if(_removableList is null)
            {
                _removableList = new List<IUpdtable>();
            }
            _removableList.Add(obj);
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

        private void LateUpdate()
        {
            if (_removableList is null || _removableList.Count == 0) return;
            for(int i = 0; i < _removableList.Count; ++i)
            {
                _updatableList.Remove(_removableList[i]);
            }
            _removableList.Clear();
        }
    }
}
