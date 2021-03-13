using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


using SmallBaseDevKit.Pool;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitVisualModule : BaseGameModule
    {
        private IPool<GameObject> _visualPool;
        protected override void CreateModule()
        {
            _visualPool = new TablePool<GameObject>();
        }

        internal GameObject GetUnitVisualByPrototype(GameObject prototypeVisual)
        {
            var resultVisual = _visualPool.GetObject(prototypeVisual);
            resultVisual.SetActive(true);
            return resultVisual;
        }

        internal void ReturnUnitVisual(GameObject unitVisual)
        {
            unitVisual.SetActive(false);
            _visualPool.ReturnObject(unitVisual);
        }
    }
}
