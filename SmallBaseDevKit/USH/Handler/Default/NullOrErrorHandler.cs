using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Handler
{
    internal sealed class NullOrErrorHandler : BaseGameHandler
    {
        public override void ExecuteHandlerLogic(IState currentState)
        {
            _Debug.Log("Null or error handler!", DebugColor.red);
            _Debug.Log($"<size=12>Invoke state - {currentState.GetType().Name}</size>", DebugColor.red);
        }

        protected override void SetupHandlerOnCreate()
        {
            _Debug.Log("NullOrErrorHandler is create!");
        }
    }
}
