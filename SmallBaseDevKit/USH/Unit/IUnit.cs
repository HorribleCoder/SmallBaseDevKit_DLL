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
        /// Создание игрового юнита с входными данными.
        /// </summary>
        /// <typeparam name="Data">Тип данных для игровго юнита.</typeparam>
        /// <param name="unitData"></param>
        void CreateUnit<Data>(Data unitData) where Data : ScriptableObject;

        ReadData ReadUnitData<ReadData>() where ReadData : ScriptableObject;

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
