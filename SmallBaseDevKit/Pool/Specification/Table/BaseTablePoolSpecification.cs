﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallBaseDevKit.Pool.Specification
{
    internal abstract class BaseTablePoolSpecification<K, T> : BasePoolSpecification<T>, IPool<T>
        where T : class
    {
        private IDictionary<K, IList<T>> _poolTable;

        internal BaseTablePoolSpecification() : base()
        {
            _poolTable = new Dictionary<K, IList<T>>();
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

                _Debug.Log($"table get key - {GetPrototypeKey(objectPrototype)}");
                if (!_poolTable.TryGetValue(GetPrototypeKey(objectPrototype), out var currentList))
                {
                    currentList = new List<T>();
                    poolObject = factory.CreateObject(objectPrototype);
                }
                else
                {
                    poolObject = currentList.FirstOrDefault();
                    if (poolObject == default)
                    {
                        poolObject = factory.CreateObject(objectPrototype);
                    }
                    currentList.Remove(poolObject);
                }
            }
            catch (Exception e)
            {
                ExceptionAction(e, objectPrototype);
            }

            return poolObject;
        }

        public void ReturnObject(T poolObject)
        {
            var key = GetPrototypeKey(poolObject);
            if (!_poolTable.TryGetValue(key, out var currenTable))
            {
                currenTable = new List<T>();
                _poolTable.Add(key, currenTable);
            }
            currenTable.Add(poolObject);
        }

        protected internal abstract K GetPrototypeKey(object prototypeObject);
    }
}