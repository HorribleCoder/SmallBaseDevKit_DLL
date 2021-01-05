namespace SmallBaseDevKit.Factory
{
    /// <summary>
    /// Интерфейс реализации фабрики объектов.
    /// </summary>
    /// <typeparam name="T">Тип объекта./></typeparam>
    internal interface IFactory<T>
        where T : class
    {
        /// <summary>
        /// Метод создания объекта по его прототипу.
        /// </summary>
        /// <param name="prototype">Прототип объекта.</param>
        /// <returns>Объект релизации прототипа.</returns>
        T CreateObject(object prototype);
    }
}
