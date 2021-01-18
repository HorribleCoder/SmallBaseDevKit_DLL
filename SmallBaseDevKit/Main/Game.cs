using UnityEngine;

using SmallBaseDevKit.Main;
using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.Unit;
using SmallBaseDevKit.USH.State;

namespace SmallBaseDevKit
{
    /// <summary>
    /// Класс для реализации быстрых команд.
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// Метод по созданию нового игрового юнита с учетом его настроек.
        /// </summary>
        /// <typeparam name="CreateUnit">Тип юнита.</typeparam>
        /// <returns>Реализация юнита.</returns>
        public static CreateUnit CreateUnit<CreateUnit>(ScriptableObject unitData) where CreateUnit: IUnit
        {
            var unit = GameInstance.Instance.GetGameModule<UnitModule>().GetUnit<CreateUnit>();
            unit.CreateUnit(unitData);
            return unit;
        }
        /// <summary>
        /// Метод по уничтожению юнита.
        /// </summary>
        /// <typeparam name="CurrentUnit"></typeparam>
        /// <param name="unit"></param>
        public static void DestroyUnit<CurrentUnit>(CurrentUnit unit) where CurrentUnit : IUnit
        {
            var i_unit = (IUnit)unit;
            i_unit.DestroyUnit();
        }
        /// <summary>
        /// Метод добавления состояния в игровую единицу.
        /// </summary>
        /// <typeparam name="AddState">Тип состояния.</typeparam>
        /// <param name="unit"></param>
        /// <param name="addStateType">Тип вставки в список.</param>
        public static void AddUnitState<AddState>(IUnit unit, AddStateType addStateType) where AddState : IState
        {
            unit.AddUnitState<AddState>(addStateType);
        }
        /// <summary>
        /// Метод удаления из игровой единицы указаногого состояния.
        /// </summary>
        /// <typeparam name="RemoveState">Тип состояния.</typeparam>
        /// <param name="unit"></param>
        public static void RemoveUnitState<RemoveState>(IUnit unit) where RemoveState : IState
        {
            unit.RemoveState<RemoveState>();
        }
    }
}
