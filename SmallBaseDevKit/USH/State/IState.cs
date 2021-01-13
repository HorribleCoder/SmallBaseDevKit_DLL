using SmallBaseDevKit.USH.Unit;

namespace SmallBaseDevKit.USH.State
{
    /// <summary>
    /// Интерфейс для всех актуальных состояний, которые может принимать игровая единица.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Первичная настройка актуального сосотяния.
        /// </summary>
        /// <param name="owner">Текущая игровая единица.</param>
        void SetupState(IUnit owner);

        /// <summary>
        /// Выполнить логику актуального состояния.
        /// </summary>
        void Execute();

        /// <summary>
        /// Проверить все условия выполнения в актуальном состояние, по умолчанию состояние не имеет заверщения.
        /// Если более 1 условия выполнения состояния, то происходит логическое умножение всех условий.
        /// <example>Как пример:
        /// <code>
        /// [<see cref="StatePredictionAttribute"/>]
        /// <code>bool SomeStatePredictionFunction(){...}</code>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        bool CheckCompliteState();

        /// <summary>
        /// Возвращение актуального состояния в пул-таблицу всех состояний.
        /// <see cref="Pool.TablePool{T}"/>
        /// </summary>
        void StateRemove();
    }
}
