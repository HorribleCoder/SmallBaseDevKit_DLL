using System;
using System.ComponentModel;

using SmallBaseDevKit.Pool;

namespace SmallBaseDevKit.GameModule
{
    /// <summary>
    /// Класс с реализацией глобального обмена сообщения в игре.
    /// Реализовано через <see cref="EventHandler"/>
    /// </summary>
    internal sealed class GlobalEventModule : BaseGameModule
    {
        private EventHandlerList _eventHandlerList;
        private IPool<EventArgs> _eventArgsPool;
        protected override void CreateModule()
        {
            _eventHandlerList = new EventHandlerList();
            _eventArgsPool = new LinePool<EventArgs>();
        }
        /// <summary>
        /// Добавление слушателя в модуль обмена сообщениями.
        /// </summary>
        /// <param name="eventKey">Уникальный ключ события, равен типу релизации <see cref="EventArgs"/></param>
        /// <param name="eventHandler">Метод обрабоки события.</param>
        #region Event Work Method
        internal void AddEvent(Type eventKey, EventHandler eventHandler)
        {
            _eventHandlerList.AddHandler(eventKey, eventHandler);
        }
        /// <summary>
        /// Обработка и выполнение события у всех слушателей.
        /// </summary>
        /// <param name="eventArgs">Данные события.</param>
        /// <param name="sender">Источник события.</param>
        internal void ExecuteEvent(EventArgs eventArgs, object sender)
        {
            var copyEvent = (EventHandler)_eventHandlerList[eventArgs.GetType()];
            if (copyEvent is null) return;
            if (sender is null) sender = this;
            copyEvent(sender, eventArgs);
        }
        /// <summary>
        /// Удаление слушателя в модуле обмена сообщениями.
        /// </summary>
        /// <param name="eventKey">Уникальный ключ события, равен типу релизации <see cref="EventArgs"/></param>
        /// <param name="eventHandler">Метод обработки события.</param>
        internal void RemoveEvent(Type eventKey, EventHandler eventHandler)
        {
            _eventHandlerList.RemoveHandler(eventKey, eventHandler);
        }
        #endregion
        /// <summary>
        /// Получаем из пула шаблон данных сообщения.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <returns></returns>
        internal T GetEventArgByType<T>() where T: EventArgs
        {
            var eventArg = _eventArgsPool.GetObject(typeof(T));
            _eventArgsPool.ReturnObject(eventArg);
            return eventArg as T;
        }
    }
}
