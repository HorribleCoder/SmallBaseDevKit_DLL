using System;

using SmallBaseDevKit.Factory;
using SmallBaseDevKit.GameException;

namespace SmallBaseDevKit.Pool.Specification
{
    /// <summary>
    /// Абстрактная релизация спецификации игровго пула.
    /// </summary>
    /// <typeparam name="T">Тип объекта./></typeparam>
    internal abstract class BasePoolSpecification<T>
        where T : class
    {
        /// <summary>
        /// Проверка входных данных на тип данных в пулле.
        /// </summary>
        /// <param name="objectPrototype">Входящий прототип.</param>
        /// <returns>Продолжаем работу с входящим прототипом.</returns>
        /// <exception cref="Exception"></exception>
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