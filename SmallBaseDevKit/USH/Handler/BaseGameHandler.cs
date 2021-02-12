using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Handler
{
    /// <summary>
    /// Базовый класс для релиазации игровой логики. Подключать нужно через атрибут <see cref="RequiredHandlerAttribute"/>.
    /// </summary>
    public abstract class BaseGameHandler : IHandler
    {
        protected BaseGameHandler()
        {
            SetupHandlerOnCreate();
        }
        public abstract void ExecuteHandlerLogic(IState currentState);
        protected abstract void SetupHandlerOnCreate();
    }
}
