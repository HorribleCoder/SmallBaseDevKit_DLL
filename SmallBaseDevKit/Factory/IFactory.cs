namespace SmallBaseDevKit.Factory
{
    /// <summary>
    /// Интерфейс реализации фабрики объектов.
    /// </summary>
    internal interface IFactory
    {
        /// <summary>
        /// Метод создания объекта согласно его типу.
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="prototype">Прототип объекта.</param>
        /// <returns>Копия объекта.</returns>
        T CreateNewObject<T>(object prototype) where T : class;
    }
}
