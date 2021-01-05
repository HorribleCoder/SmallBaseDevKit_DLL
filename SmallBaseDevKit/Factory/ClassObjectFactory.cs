using System;

namespace SmallBaseDevKit.Factory
{
    /// <summary>
    /// Спецификация фабрики по созданию объектов типа C# класса.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    internal sealed class ClassObjectFactory<T> : IFactory<T>
        where T : class
    {
        internal ClassObjectFactory()
        {
            _Debug.Log("Create class factoruy!");
        }

        public T CreateObject(object prototype)
        {
            object newObj = Activator.CreateInstance((Type)prototype);
            return (T)newObj;
        }
    }
}
