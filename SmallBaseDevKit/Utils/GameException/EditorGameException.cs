using System;
using UnityEngine;
using UnityEditor;

namespace SmallBaseDevKit.GameException
{
    /// <summary>
    /// Релизация обработки ошибки по умолчанию - <see cref="UnityEditor"/>.
    /// </summary>
    internal sealed class EditorGameException : IGameException
    {
        //Остановка редактора и вывод ошибки в консоль.
        public void ExceptionProcessExecute(Exception e, string msg = null)
        {
            Debug.Log(e.StackTrace);
            Debug.unityLogger.LogException(e);
            Debug.Break();
            EditorApplication.isPlaying = false;
        }
    }
}
