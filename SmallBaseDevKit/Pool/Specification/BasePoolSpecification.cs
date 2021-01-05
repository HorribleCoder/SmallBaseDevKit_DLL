using System;

using SmallBaseDevKit.Factory;
using SmallBaseDevKit.GameException;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Абстрактная релизация спецификации игровго пула.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    internal abstract class BasePoolSpecification<T>
        where T : class
    {
        /// <summary>
        /// Ссылка на фабрику для создания объектов согласно специфики.
        /// </summary>
        protected internal IFactory<T> factory;
        internal BasePoolSpecification()
        {
            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                factory = new UnityObjectFactory<T>();
            }
            else
            {
                factory = new ClassObjectFactory<T>();
            }
        }
        /// <summary>
        /// Проверка входных данных на тип данных в пулле.
        /// </summary>
        /// <param name="objectPrototype">Входящий прототип.</param>
        /// <returns>Продолжаем работу с входящим прототипом.</returns>
        /// <exception cref="Формируем ошбику если входной протип не совпадает с хранящимся типом данных в пуле."></exception>
        protected internal abstract bool CheckInputPrototypeData(object objectPrototype);
        /// <summary>
        /// Обработка ошибки в пулле через - 
        /// <see cref="ExceptionHandler"/>
        /// </summary>
        /// <param name="e">Ошибка.</param>
        /// <param name="objectPrototype">Входящий прототип.</param>
        protected internal void ExceptionAction(Exception e, object objectPrototype)
        {
            string prototypeName = (objectPrototype is Type) ? objectPrototype.ToString() : objectPrototype.GetType().ToString();
            ExceptionHandler.ExceptionProcessExecute(e, $"Invalid input data type <b>{prototypeName}</b> - use in pool data type <b>{typeof(T)}</b>");
        }
    }
}