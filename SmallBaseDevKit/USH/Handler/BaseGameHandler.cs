using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Handler
{
    /// <summary>
    /// Базовый класс для релиазации игровой логики. Подключать нужно через атрибут <see cref="RequiredHandlerAttribute"/>.
    /// </summary>
    public abstract class BaseGameHandler : IHandler
    {
        public abstract void ExecuteHandlerLogic(IState currentState);

        /// <summary>
        /// Метод конвертации входящего состояния в рабочее.
        /// </summary>
        /// <typeparam name="T">Тип состояния с которым будет работать логика.</typeparam>
        /// <param name="currenState">Входящее состояние.</param>
        /// <returns>Актуальное состояние.</returns>
        protected T ConvertStateToType<T>(IState currenState) where T: IState
        {
            return (T)currenState;
        }
    }
}
