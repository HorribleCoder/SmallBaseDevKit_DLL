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
                if (typeof(T).IsInterface)
                {
                    var t = (Type)objectPrototype;
                    var interfaces = t.GetInterfaces();
                    bool result = false;
                    for (int i = 0; i < interfaces.Length; ++i)
                    {
                        if (interfaces[i].Equals(typeof(T)))
                        {
                            result = true;
                            break;
                        }
                    }
                    return result;
                }
                else
                {
                    var type = (Type)objectPrototype;
                    return type.IsSubclassOf(typeof(T));
                }
            }
        }

        protected internal override bool EqualObjectPrediction(object pivotObject, object checkObject)
        {
            bool result = false;
            if (pivotObject is Type)
            {
                result = (Type)pivotObject == checkObject.GetType();
            }
            else
            {
                result = pivotObject.GetType() == checkObject.GetType();
            }
            return result;
        }
    }
}
