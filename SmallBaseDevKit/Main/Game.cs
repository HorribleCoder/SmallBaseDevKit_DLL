using System;
using UnityEngine;

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
        #region Unit

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
            if(!unit.TryGetUnitState<AddState>(out var state))
            {
                var module = GameInstance.Instance.GetGameModule<UnitStateModule>();
                module.AddStateInAwaitList(unit, module.GetState<AddState>(), addStateType);
            }
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
        /// <summary>
        /// Метод получения конктерной игровой единицы используя отличительные характеристики(GameObject, Collider и т.д.). 
        /// <para>!!!Только для наследников <see cref="BaseUnit{UnitData}"/>!!!</para>
        /// </summary>
        /// <typeparam name="UnitKey">Тип отличительной характеристики.</typeparam>
        /// <param name="unitKey">Отличительная характеристика.</param>
        /// <returns></returns>
        public static IUnit GetUnitInRegistor<UnitKey>(UnitKey unitKey) where UnitKey : class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitInRegistor(unitKey);
        }
        /// <summary>
        /// Метод получения числа игровых единиц по регистрационому типу.
        /// </summary>
        /// <typeparam name="KeyType"></typeparam>
        /// <returns>Число игровых единицна данный момент.</returns>
        public static int GetUnitCountInRegistor<KeyType>() where KeyType : class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitCountInRegistorByType<KeyType>();
        }
        /// <summary>
        /// Получить текущий индекс игровой единицы в регистре.
        /// </summary>
        /// <typeparam name="T">Тип ключа в регистре.</typeparam>
        /// <param name="unit">Конретная игровая единица.</param>
        /// <returns>Индекс.</returns>
        public static int GetUnitIndexInRegistor<T>(IUnit unit) where T: class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitIndexInRegistor<T>(unit);
        }

        /// <summary>
        /// Метод сброса регистра игровых единиц.
        /// </summary>
        public static void ResetUnitRegistor()
        {
            GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().ResetUnitRegistor();
        }

        #endregion

        #region Event
        /// <summary>
        /// Добавить слушателя событий в модуль сообщений.
        /// </summary>
        /// <typeparam name="AddEventType">Тип события.</typeparam>
        /// <param name="eventHandler">Метод обработки сообщения.</param>
        public static void AddEventListner<AddEventType>(EventHandler eventHandler) where AddEventType : EventArgs
        {
            GameInstance.Instance.GetGameModule<GlobalEventModule>().AddEvent(typeof(AddEventType), eventHandler);
        }
        /// <summary>
        /// Выполнить событие в модуле сообщений.
        /// </summary>
        /// <typeparam name="ExecuteEventArg">Тип события.</typeparam>
        /// <param name="eventArgSetupCallback">Метод настройки события.</param>
        /// <param name="sender">Отправитель сообщения.</param>
        public static void ExecuteEvent<ExecuteEventArg>(object sender = null, Action<ExecuteEventArg> eventArgSetupCallback = null) where ExecuteEventArg : EventArgs
        {
            var eventArgs = GameInstance.Instance.GetGameModule<GlobalEventModule>().GetEventArgByType<ExecuteEventArg>();
            eventArgSetupCallback?.Invoke(eventArgs);
            GameInstance.Instance.GetGameModule<GlobalEventModule>().ExecuteEvent(eventArgs, sender);
        }
        /// <summary>
        /// Удалить слушателя из модуля сообщений.
        /// </summary>
        /// <typeparam name="RemoveEventType">Тип сообщения.</typeparam>
        /// <param name="eventHandler">Метод обработки сообщения.</param>
        public static void RemoveEventListner<RemoveEventType>(EventHandler eventHandler) where RemoveEventType : EventArgs
        {
            GameInstance.Instance.GetGameModule<GlobalEventModule>().RemoveEvent(typeof(RemoveEventType), eventHandler);
        }

        #endregion
    }
}
