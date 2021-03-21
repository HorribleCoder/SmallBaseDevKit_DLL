using System;
using UnityEngine;

using SmallBaseDevKit.USH.Handler;

namespace SmallBaseDevKit.USH.State
{
    [RequiredHandler(typeof(TimerHandler))]
    public abstract class TimerState : BaseUnitState<(float time, Action timerCallback)>
    {
        private float _time;

        public override void Deconstruct(out (float time, Action timerCallback) stateParam)
        {
            stateParam.time = _time;
            stateParam.timerCallback = TimeCallback;
        }

        protected override void ExtendedSetupState()
        {
            _time = Time.time + SetTimer();
        }

        protected abstract float SetTimer();
        protected abstract void TimeCallback();
    }
}
