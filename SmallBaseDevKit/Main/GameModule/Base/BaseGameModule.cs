namespace SmallBaseDevKit.GameModule
{
    /// <summary>
    /// Базовый класс для всех игровых модулей. Обращение к реализации модуля через 
    /// <see cref="Main.GameInstance"/>.
    /// </summary>
    public abstract class BaseGameModule
    {
        public BaseGameModule()
        {
            CreateModule();
        }
        protected abstract void CreateModule();
    }
}
