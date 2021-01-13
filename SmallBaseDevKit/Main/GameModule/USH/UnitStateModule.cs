using SmallBaseDevKit.Pool;
using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitStateModule : BaseGameModule
    {
        private IPool<IState> _statePool;

        protected override void CreateModule()
        {
            _statePool = new TablePool<IState>();
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
    }
}
