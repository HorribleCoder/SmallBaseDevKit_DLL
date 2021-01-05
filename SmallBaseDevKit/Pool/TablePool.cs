using SmallBaseDevKit.Pool.Specification;

namespace SmallBaseDevKit.Pool
{
    /// <summary>
    /// Пул-таблица для хранения большого числа объектов.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    public sealed class TablePool<T> : IPool<T>
        where T : class
    {
        private IPool<T> _poolSpecification;

        public TablePool()
        {
            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                _poolSpecification = new TableByName<T>();
            }
            else
            {
                _poolSpecification = new TableByType<T>();
            }
        }

        public T GetObject(object objectPrototype)
        {
            return _poolSpecification.GetObject(objectPrototype);
        }

        public void ReturnObject(T poolObject)
        {
            _poolSpecification.ReturnObject(poolObject);
        }
        public void PoolDebugView()
        {
            _poolSpecification.PoolDebugView();
        }
    }
}
