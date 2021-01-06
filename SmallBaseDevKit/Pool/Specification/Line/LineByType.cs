using System;


namespace SmallBaseDevKit.Pool.Specification
{
    internal sealed class LineByType<T> : BaseLinePoolSpecification<T>
        where T : class
    {
        protected internal override bool CheckInputPrototypeData(object objectPrototype)
        {
            if (objectPrototype is null)
            {
                return false;
            }
            if ((Type)objectPrototype == typeof(T))
            {
                return true;
            }
            else
            {
                var type = (Type)objectPrototype;
                return type.IsSubclassOf(typeof(T));
            }
        }

        protected internal override bool EqualObjectPrediction(object pivotObject, object checkObject)
        {
            return (Type)pivotObject == checkObject.GetType();
        }
    }
}
