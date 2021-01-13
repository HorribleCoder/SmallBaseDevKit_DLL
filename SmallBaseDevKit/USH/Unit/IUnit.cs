using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Интрефейс для всех игровых единиц со сложной логикой через актуальные состояния.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Создание игрового юнита.
        /// </summary>
        void CreateUnit();

        /// <summary>
        /// Добавить к игровой единице текущее состояние.
        /// </summary>
        void AddUnitState<T>() where T: IState;
        
        /// <summary>
        /// Получить актуальное состояние по его типу.
        /// </summary>
        /// <typeparam name="S">Тип состояния.</typeparam>
        /// <returns>Актуальное состояние.</returns>
        IState GetUnitState<S>() where S : IState;

        /// <summary>
        /// Получить Unity-компнент по его типу.
        /// </summary>
        /// <typeparam name="C">Тип компонента.</typeparam>
        /// <returns>Unity-компонент.</returns>
        UnityEngine.Object GetUnitComponent<C>() where C : UnityEngine.Object;

        /// <summary>
        /// Удалить у игровой единицы текущее состояние.
        /// </summary>
        /// <param name="state">Состояние.</param>
        void RemoveState(IState state);

        /// <summary>
        /// Удаление игрового юнита.
        /// </summary>
        void DestroyUnit();

        /// <summary>
        /// Обновить все состояния юнита выполняется в независимости от методов OnUpdate() и OnFixedUpdate().
        /// </summary>
        void UpdateUnitState();
    }
}
