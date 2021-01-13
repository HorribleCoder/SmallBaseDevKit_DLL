namespace SmallBaseDevKit
{
    /// <summary>
    /// Интерфейс для runtime-обновляемых объектов в игре. Обновление через единный помощник - GameUpdateHandler.
    /// </summary>
    public interface IUpdtable
    {
        void OnUpdate();
        void OnFixedUpdate();
    }
}
