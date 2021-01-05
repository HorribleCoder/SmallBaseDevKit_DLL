using System;

namespace SmallBaseDevKit.GameException
{
    /// <summary>
    /// Интерфейс релизации обработки ошибки в зависимости от платформы.
    /// </summary>
    public interface IGameException
    {
        void ExceptionProcessExecute(Exception e, string msg = null);
    }
}
