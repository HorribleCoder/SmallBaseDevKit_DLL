using System;

namespace SmallBaseDevKit.GameException
{
    /// <summary>
    /// Интерфейс релизации обработки ошибки в зависимости от платформы.
    /// </summary>
    public interface IGameException
    {
        /// <summary>
        /// Метод обработки игровой ошибки с учетом спецификации.
        /// </summary>
        /// <param name="e">Ошибка.</param>
        /// <param name="msg">Дополнительное сообщение.</param>
        void ExceptionProcessExecute(Exception e, string msg = null);
    }
}
