using UnityEngine;
using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Интрефейс для всех игровых единиц со сложной логикой через актуальные состояния.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Активна ли игровая единица.
        /// </summary>
        bool isActive { get; }

        /// <summary>
        /// Создание игрового юнита.
        /// </summary>
        void CreateUnit();

        /// <summary>
        /// Добавить к игровой единице текущее состояние.
        /// </summary>
        void AddUnitState(IState state, AddStateType addStateType);
        /// <summary>
        /// Получить актуальное состояние по его типу.
        /// </summary>
        /// <typeparam name="State">Тип состояния.</typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        bool TryGetUnitState<State>(out State state) where State : IState;

        /// <summary>
        /// Удалить у игровой единицы текущее состояние.
        /// </summary>
        /// <typeparam name="RemoveState"></typeparam>
        void RemoveState<RemoveState>() where RemoveState : IState;

        /// <summary>
        /// Получить Unity-компнент по его типу.
        /// </summary>
        /// <typeparam name="C">Тип компонента.</typeparam>
        /// <returns>Unity-компонент.</returns>
        bool TryGetUnitComponent<C>(out C component) where C : Component;

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
