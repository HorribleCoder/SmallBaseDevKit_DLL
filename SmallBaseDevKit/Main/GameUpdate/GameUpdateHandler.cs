using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using SmallBaseDevKit.USH.Unit;


namespace SmallBaseDevKit.Main
{
    public sealed class GameUpdateHandler : MonoSingleton<GameUpdateHandler>, IRegistrator<IUpdatable>
    {
        private event Action OnUpdataeEvent;
        private event Action OnFixedUpdateEvent;

        internal delegate void InvokeLateUpdateSubprocess();
        internal event InvokeLateUpdateSubprocess invokeLateUpdateSubprocessEvent;

        public void Registration(IUpdatable obj)
        {
            if(obj is IUnit)
            {
                var c_obj = obj as IUnit;
                OnUpdataeEvent += c_obj.UpdateUnitState;
            }
            OnUpdataeEvent += obj.OnUpdate;
            OnFixedUpdateEvent += obj.OnFixedUpdate;
        }

        public void Unregistration(IUpdatable obj)
        {
            if (obj is IUnit)
            {
                var c_obj = obj as IUnit;
                OnUpdataeEvent -= c_obj.UpdateUnitState;
            }
            OnUpdataeEvent -= obj.OnUpdate;
            OnFixedUpdateEvent -= obj.OnFixedUpdate;
        }

        private void Start()
        {
            StartCoroutine(LateAddStateForUnitSubprocess());
        }

        private void Update()
        {
            OnUpdataeEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdateEvent?.Invoke();
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
