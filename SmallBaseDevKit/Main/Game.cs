using System;
using UnityEngine;

using SmallBaseDevKit.GameModule;
using SmallBaseDevKit.USH.Unit;
using SmallBaseDevKit.USH.State;
using SmallBaseDevKit.Factory;

namespace SmallBaseDevKit
{
    /// <summary>
    /// Класс для реализации быстрых команд.
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// Создать экземпляр объекта ссылаясь на прототип.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="prototype">Прототип.</param>
        /// <returns></returns>
        public static T CreateNewObject<T>(object prototype) where T: class
        {
            return (T)GameFactory.Instance.CreateNewObject<T>(prototype);
        }

        #region Unit
        /// <summary>
        /// Метод по созданию нового игрового юнита с учетом его настроек.
        /// </summary>
        /// <typeparam name="T">Тип юнита.</typeparam>
        /// <returns>Реализация юнита.</returns>
        public static T CreateUnit<T>(ScriptableObject unitData) where T: IUnit
        {
            var unit = GameInstance.Instance.GetGameModule<UnitModule>().GetUnit<T>();
            unit.CreateUnit(unitData);
            return unit;
        }
        /// <summary>
        /// Метод по уничтожению юнита.
        /// </summary>
        /// <param name="unit"></param>
        public static void DestroyUnit(IUnit unit)
        {
            unit.DestroyUnit();
        }
        /// <summary>
        /// Метод добавления состояния в игровую единицу.
        /// </summary>
        /// <typeparam name="T">Тип состояния.</typeparam>
        /// <param name="unit"></param>
        /// <param name="addStateType">Тип вставки в список.</param>
        public static void AddUnitState<T>(IUnit unit, AddStateType addStateType) where T : IState
        {
            if(!unit.TryGetUnitState<T>(out var state))
            {
                var module = GameInstance.Instance.GetGameModule<UnitStateModule>();
                module.AddStateInAwaitList(unit, module.GetState<T>(), addStateType);
            }
        }
        /// <summary>
        /// Метод удаления из игровой единицы указаногого состояния.
        /// </summary>
        /// <typeparam name="T">Тип состояния.</typeparam>
        /// <param name="unit"></param>
        public static void RemoveUnitState<T>(IUnit unit) where T : IState
        {
            unit.RemoveState<T>();
        }
        #endregion

        #region Unit Hash Registor
        /// <summary>
        /// Метод регистрации игровой единицы по отличительной хаарктеристики( GameObject, Collider и т.д.)
        /// </summary>
        /// <typeparam name="T">Тип характеристики.</typeparam>
        /// <param name="registrationType">Отличительная характеристика.</param>
        /// <param name="unit">Текущая игровая единица.</param>
        public static void AddUnitInRegistorBySpecificType<T>(T registrationType, IUnit unit) where T: class
        {
            GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().RegistrationUnit<T>(registrationType, unit);
        }
        /// <summary>
        /// Метод получения конктерной игровой единицы используя отличительные характеристики(GameObject, Collider и т.д.). 
        /// </summary>
        /// <typeparam name="T">Тип отличительной характеристики.</typeparam>
        /// <param name="unitKey">Отличительная характеристика.</param>
        /// <returns></returns>
        public static IUnit GetUnitInRegistor<T>(T unitKey) where T : class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitInRegistor(unitKey);
        }
        /// <summary>
        /// Метод получения числа игровых единиц по регистрационому типу.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Число игровых единицна данный момент.</returns>
        public static int GetUnitCountInRegistor<T>() where T : class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitCountInRegistorByType<T>();
        }
        /// <summary>
        /// Получить текущий индекс игровой единицы в регистре.
        /// </summary>
        /// <typeparam name="T">Тип ключа в регистре.</typeparam>
        /// <param name="unit">Конретная игровая единица.</param>
        /// <returns>Индекс.</returns>
        public static int GetUnitIndexInRegistor<T>(IUnit unit) where T : class
        {
            return GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().GetUnitIndexInRegistor<T>(unit);
        }

        /// <summary>
        /// Выполнить указанное действие для всех игровых единиц в регистре по ключ-типу.
        /// </summary>
        /// <typeparam name="T">Ключ тип.</typeparam>
        /// <param name="action">Действие с игровой единицей.</param>
        public static void ForEachAllUnitInRegistor<T>(Action<IUnit> action) where T : class
        {
            GameInstance.Instance.GetGameModule<UnitHashRegistorModule>().ExecuteSomeActionWithAllUnitInRegistor<T>(action);
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
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="eventHandler">Метод обработки сообщения.</param>
        public static void AddEventListner<T>(EventHandler eventHandler) where T : EventArgs
        {
            GameInstance.Instance.GetGameModule<GlobalEventModule>().AddEvent(typeof(T), eventHandler);
        }
        /// <summary>
        /// Выполнить событие в модуле сообщений.
        /// </summary>
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="eventArgSetupCallback">Метод настройки события.</param>
        /// <param name="sender">Отправитель сообщения.</param>
        public static void ExecuteEvent<T>(object sender = null, Action<T> eventArgSetupCallback = null) where T : EventArgs
        {
            var eventArgs = GameInstance.Instance.GetGameModule<GlobalEventModule>().GetEventArgByType<T>();
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
