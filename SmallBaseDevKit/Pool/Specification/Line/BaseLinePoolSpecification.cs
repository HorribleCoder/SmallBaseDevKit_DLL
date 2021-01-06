using System;
using System.Collections.Generic;
using SmallBaseDevKit.Factory;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Абстрактная спецификация для пула-списка.
    /// </summary>
    /// <typeparam name="T">Тип объекта./></typeparam>
    internal abstract class BaseLinePoolSpecification<T> : BasePoolSpecification<T>, IPool<T>
        where T : class
    {
        private LinkedList<T> _poolList;

        internal BaseLinePoolSpecification() : base()
        {
            _poolList = new LinkedList<T>();
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
                if (!GameUtiles.TryGetObjectInLinkedList(_poolList, EqualObjectPrediction, objectPrototype, out poolObject))
                {
                    poolObject = GameFactory.Instance.CreateNewObject<T>(objectPrototype);
                }
                _poolList.Remove(poolObject);
            }
            catch (Exception e)
            {
                ExceptionAction(e, objectPrototype);
            }
            return poolObject;
        }
        public void ReturnObject(T poolObject)
        {
            if(!GameUtiles.ContainObjectInLinkedList(_poolList, EqualObjectPrediction, poolObject))
            {
                _poolList.AddFirst(poolObject);
            }
        }

        public void PoolDebugView()
        {
            _Debug.Log("Debug line pool by specific!", DebugColor.blue);
            _Debug.Log($"Pool type - {typeof(T).Name}", DebugColor.green);
            _Debug.Log($"Objects count = {_poolList.Count}");
            var node = _poolList.First;
            for(int i = 0; i < _poolList.Count; ++i)
            {
                _Debug.Log(node.Value);
                node = node.Next;
            }
        }
        /// <summary>
        /// Условие проверки соотвествия двух объектов в пуле
        /// </summary>
        /// <param name="pivotObject"></param>
        /// <param name="checkObject"></param>
        /// <returns></returns>
        protected internal abstract bool EqualObjectPrediction(object pivotObject, object checkObject);
    }
}