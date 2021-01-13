using System;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Спецификация пулла где ключем является Type объекта.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class TableByType<T> : BaseTablePoolSpecification<Type, T>
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
                    for(int i = 0; i < interfaces.Length; ++i)
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

        protected internal override Type GetPrototypeKey(object prototypeObject)
        {
            if (prototypeObject is Type)
            {
                return prototypeObject as Type;
            }
            else
            {
                return prototypeObject.GetType();
            }
        }
    }
}
