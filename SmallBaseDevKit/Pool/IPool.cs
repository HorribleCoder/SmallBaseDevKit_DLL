namespace SmallBaseDevKit.Pool
{
    /// <summary>
    /// Интерфейс игрового пула.
    /// </summary>
    /// <typeparam name="T">Тип объекта, формат - <see cref="class"/></typeparam>
    public interface IPool<T>
        where T : class
    {
        /// <summary>
        /// Получаем объект из пула. Объект удаляется из пула.
        /// </summary>
        /// <param name="objectPrototype">Прототип объекта.</param>
        /// <returns>Объект пула.</returns>
        T GetObject(object objectPrototype);
        /// <summary>
        /// Возвращения объекта в пул. 
        /// </summary>
        /// <param name="poolObject">Текущий объект.</param>
        void ReturnObject(T poolObject);

        /// <summary>
        /// Метод проверки наполенения пулла для дебага.
        /// </summary>
        void PoolDebugView();
    }
}
