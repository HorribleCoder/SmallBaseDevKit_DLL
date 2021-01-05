using UnityEngine;

namespace SmallBaseDevKit.Factory
{
    /// <summary>
    /// Спецификация фабрики по созданию объектов типа MonoBehaviour или UnityEngine.Object.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    internal sealed class UnityObjectFactory<T> : IFactory<T>
        where T : class
    {
        internal UnityObjectFactory()
        {
            _Debug.Log("Create unity GO factory!");
        }

        public T CreateObject(object prototype)
        {
            var prt = (UnityEngine.Object)prototype;
            var go = MonoBehaviour.Instantiate(prt);
            go.name = prt.name;
            object newObj = go;
            return (T)newObj;
        }
    }
}
