using UnityEngine;
using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Handler
{
    internal sealed class TimerHandler : BaseGameHandler
    {
        public override void ExecuteHandlerLogic(IState currentState)
        {
            currentState.ConvertTo<TimerState>().Deconstruct(out var stateParam);
            if(stateParam.time <= Time.time)
            {
                stateParam.timerCallback?.Invoke();
            }
        }

        protected override void SetupHandlerOnCreate()
        {

        }
    }
}
