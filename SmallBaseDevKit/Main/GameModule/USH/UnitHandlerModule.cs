using System;

using SmallBaseDevKit.Pool;
using SmallBaseDevKit.USH.Handler;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitHandlerModule : BaseGameModule
    {
        private IPool<IHandler> _handlersPool;

        protected override void CreateModule()
        {
            _handlersPool = new LinePool<IHandler>();
        }

        public IHandler GetHandler(Type handlerType)
        {
            var handler = _handlersPool.GetObject(handlerType);
            _handlersPool.ReturnObject(handler);
            return handler;
        }

        public override void DebugModule()
        {
            base.DebugModule();
            _handlersPool.PoolDebugView();
        }
    }
}
