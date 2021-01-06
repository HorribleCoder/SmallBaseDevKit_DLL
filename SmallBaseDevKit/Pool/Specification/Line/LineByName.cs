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

        protected internal override bool EqualObjectPrediction(object pivotObject, object checkObject)
        {
            var pivotGO = (UnityEngine.Object)pivotObject;
            var equalGO = (UnityEngine.Object)checkObject;
            return String.Equals(pivotGO.name, equalGO.name);
        }
    }
}
