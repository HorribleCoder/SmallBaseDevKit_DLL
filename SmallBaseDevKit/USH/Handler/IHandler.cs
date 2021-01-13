using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Handler
{
    /// <summary>
    /// Интерфейс для реализации игровой логики.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Выполнить логику 
        /// </summary>
        /// <param name="currentState"></param>
        void ExecuteHandlerLogic(IState currentState);
    }
}
