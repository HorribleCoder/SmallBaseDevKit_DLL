using System;
using System.Collections.Generic;
using System.Text;

using SmallBaseDevKit.Main;
using SmallBaseDevKit.Pool;
using SmallBaseDevKit.USH.Unit;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitModule : BaseGameModule
    {
        private IPool<IUnit> _unitPool;
        protected override void CreateModule()
        {
            _unitPool = new TablePool<IUnit>();
        }

        public override void DebugModule()
        {
            base.DebugModule();
            _unitPool.PoolDebugView();
        }

        internal T GetUnit<T>() where T: IUnit
        {
            return (T)_unitPool.GetObject(typeof(T));
        }


        internal void ReturnUnit<T>(T unit) where T: IUnit
        {
            _unitPool.ReturnObject(unit);
        } 
    }
}
