using System;
using System.Collections.Generic;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Абстрактная спецификация для пула-списка.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    internal abstract class BaseLinePoolSpecification<T> : BasePoolSpecification<T>, IPool<T>
        where T : class
    {
        protected internal List<T> poolList;

        internal BaseLinePoolSpecification() : base()
        {
            poolList = new List<T>();
        }
        public T GetObject(object objectPrototype)
        {
            T poolObject = default;
            try
            {
                if (!CheckInputPrototypeData(objectPrototype))
                {
                    throw new Exception();
                }
                if (!TryGetObjectInPool(objectPrototype, out poolObject))
                {
                    poolObject = factory.CreateObject(objectPrototype);
                }
                poolList.Remove(poolObject);
            }
            catch (Exception e)
            {
                ExceptionAction(e, objectPrototype);
            }
            return poolObject;
        }
        public void ReturnObject(T poolObject)
        {
            if (!FindObjectInPool(poolObject))
            {
                poolList.Add(poolObject);
            }
        }

        public void PoolDebugView()
        {
            _Debug.Log("Debug line pool by specific!", DebugColor.blue);
            _Debug.Log($"Pool type - {typeof(T).Name}", DebugColor.green);
            _Debug.Log($"Objects count = {poolList.Count}");
            for(int i = 0; i < poolList.Count; ++i)
            {
                _Debug.Log(poolList[i].ToString());
            }
        }

        protected internal abstract bool TryGetObjectInPool(object objectPrototype, out T poolObject);

        protected internal abstract bool FindObjectInPool(T searchObject);
    }
}