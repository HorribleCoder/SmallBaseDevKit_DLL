using SmallBaseDevKit.Pool.Specification;

namespace SmallBaseDevKit.Pool
{
    /// <summary>
    /// Пул-список для многоразовых объектов в единичном экзепляре.
    /// </summary>
    /// <typeparam name="T">Тип объекта./></typeparam>
    public sealed class LinePool<T> : IPool<T>
        where T : class
    {
        private IPool<T> _poolSpecification;

        public LinePool()
        {
            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                _poolSpecification = new LineByName<T>();
            }
            else
            {
                _poolSpecification = new LineByType<T>();
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
