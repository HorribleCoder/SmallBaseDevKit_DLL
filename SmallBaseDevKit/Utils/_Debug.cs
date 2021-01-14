/// <summary>
/// Тип цвета для сообщения.
/// </summary>
public enum DebugColor
{
    black,
    red,
    purple,
    blue,
    green,
    orange,
    white,
}
/// <summary>
/// Класс по релизации вывод сообщений с использованеим цвета.
/// </summary>
public static class _Debug
{
    /// <summary>
    /// Метод вывода сообщения в консоль.
    /// </summary>
    /// <param name="message">Объект сообщения.</param>
    /// <param name="color">Цвет сообщения.</param>
    public static void Log(object message, DebugColor color = DebugColor.white)
    {
        UnityEngine.Debug.Log($"<color={color.ToString()}><size=12><color=white>CUSTOM_DEBUG >>></color></size> Message - {message}.</color>");
    }
}
