using System;

namespace SmallBaseDevKit.GameException
{
    /// <summary>
    /// Обработчик ошибки в игре.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Эвент по обработке ошибки взависимости от платформы, по умолчанию - <see cref="EditorGameException"/>.
        /// </summary>
        private static readonly Action<Exception, string> ExceptionEvent;

        static ExceptionHandler()
        {
            //create default error handler - unity editor
            IGameException currentException = new EditorGameException();
            ExceptionEvent += currentException.ExceptionProcessExecute;
        }
        /// <summary>
        /// Метод обработки ошибки.
        /// </summary>
        /// <param name="e">Ошибка.</param>
        /// <param name="msg">Дополнительное сообщение..</param>
        public static void ExceptionProcessExecute(Exception e, string msg = null)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                _Debug.Log("ERROR", DebugColor.red);
                _Debug.Log(msg);
            }
            ExceptionEvent?.Invoke(e, msg);
        }
    }
}

