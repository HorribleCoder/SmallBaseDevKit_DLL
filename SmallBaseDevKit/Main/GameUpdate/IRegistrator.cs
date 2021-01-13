namespace SmallBaseDevKit
{
    /// <summary>
    /// Интрефейс для регистра чего либо.
    /// </summary>
    /// <typeparam name="T">Тип объект, что будем регистрировать.</typeparam>
    public interface IRegistrator<T>
        where T: class
    {
        /// <summary>
        /// Добавляем объект в регистор.
        /// </summary>
        /// <param name="obj"></param>
        void Registration(T obj);
        /// <summary>
        /// Удаляем объект из регистра.
        /// </summary>
        /// <param name="obj"></param>
        void Unregistration(T obj);
    }
}
