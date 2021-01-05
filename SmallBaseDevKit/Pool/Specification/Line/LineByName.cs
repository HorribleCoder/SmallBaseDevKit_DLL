using System;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Спецификация пула-списка по хранению объектов по имени.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class LineByName<T> : BaseLinePoolSpecification<T>
        where T : class
    {
        protected internal override bool CheckInputPrototypeData(object objectPrototype)
        {
            if (objectPrototype is null)
            {
                return false;
            }
            return objectPrototype.GetType() == typeof(T);
        }

        protected internal override bool FindObjectInPool(T searchObject)
        {
            bool result = false;
            for (int i = 0; i < poolList.Count; ++i)
            {
                if (_Equal(poolList[i], searchObject))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        protected internal override bool TryGetObjectInPool(object objectPrototype, out T poolObject)
        {
            bool result = false;
            poolObject = default;
            for (int i = 0; i < poolList.Count; ++i)
            {
                if (_Equal(objectPrototype, poolList[i]))
                {
                    result = true;
                    poolObject = poolList[i];
                    break;
                }
            }

            return result;
        }

        private bool _Equal(object pivotObject, object equalObject)
        {
            var pivotGO = (UnityEngine.Object)pivotObject;
            var equalGO = (UnityEngine.Object)equalObject;
            return String.Equals(pivotGO.name, equalGO.name);
        }
    }
}
