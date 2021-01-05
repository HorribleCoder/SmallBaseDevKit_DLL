using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Абстракция по специкации пула-таблицы.
    /// </summary>
    /// <typeparam name="K">Уникальный ключ согласно спецификации.</typeparam>
    /// <typeparam name="T">Тип объекта./></typeparam>
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
        public void PoolDebugView()
        {
            _Debug.Log("Debug table pool by specific!", DebugColor.blue);
            _Debug.Log($"Pool type - {typeof(T).Name}", DebugColor.green);
            int listCount = 0;
            int count = 0;
            foreach(var list in _poolTable)
            {
                _Debug.Log($"List uniq key = {list.Key}");
                listCount++;
                for(int i = 0; i < list.Value.Count; ++i)
                {
                    _Debug.Log(list.Value[i].ToString());
                    count++;
                }
            }
            _Debug.Log("Total:", DebugColor.blue);
            _Debug.Log($"Total list count = {listCount}", DebugColor.green);
            _Debug.Log($"Total objects count = {count}", DebugColor.green);

        }
        /// <summary>
        /// Метод по обработке прототипа и получения уникального ключа для хранения объектов. 
        /// </summary>
        /// <param name="prototypeObject">Протип объекта.</param>
        /// <returns>Уникальный ключ согласно спецификации.</returns>
        protected internal abstract K GetPrototypeKey(object prototypeObject);
    }
}
