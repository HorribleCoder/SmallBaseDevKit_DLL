using System;
using System.Reflection;
using System.Collections.Generic;

using SmallBaseDevKit.USH.Unit;
using SmallBaseDevKit.USH.Handler;
using SmallBaseDevKit.GameModule;

namespace SmallBaseDevKit.USH.State
{
    /// <summary>
    ///  Базовый класс для описания всех актуальных состояний, которые может принимать игровоя единица.
    ///  Для метода завершения состояния - <see cref="IState.CheckCompliteState"/>, необходимо в тело класса добавить функции с атрибутом [<see cref="StatePredictionAttribute"/>]
    /// </summary>
    /// <typeparam name="T">Данные состояния.</typeparam>
    public abstract class BaseUnitState<T> : IState
    {
        protected IUnit owner;
        private Type _handlerType;
        private IList<Func<bool>> _predictionList;

        protected BaseUnitState()
        {
            _predictionList = new List<Func<bool>>();
            SetupHandler();
            SetupStatePrediction();
        }

        #region Abstract Method
        /// <summary>
        /// Метод по получению хранящихся данных в актуальномы состояние.
        /// </summary>
        /// <param name="stateParam">Конкретный тип данных или <see cref="ValueTuple"/>.</param>
        public abstract void Deconstruct(out T stateParam);
        /// <summary>
        /// Метод расширенных настроек потомков состояния.
        /// <para>Вызвается после получения ссылки на игровую единицу.</para>
        /// </summary>
        protected abstract void ExtendedSetupState();
        /// <summary>
        /// Метод расширенного удаления потомков состояния.
        /// <para>Вызывается после отправки состояния в пул.</para>
        /// </summary>
        protected abstract void ExtendedStateComplite();
        #endregion

        #region Interface Method Realase
        void IState.SetupState(IUnit owner)
        {
            this.owner = owner;
            ExtendedSetupState();
        }

        void IState.Execute()
        {
            var handler = GameInstance.Instance.GetGameModule<UnitHandlerModule>().GetHandler(_handlerType);
            handler.ExecuteHandlerLogic(this);
        }

        bool IState.CheckCompliteState()
        {
            bool result = false;
            if(_predictionList.Count > 0)
            {
                result = true;
                for(int i = 0; i < _predictionList.Count; ++i)
                {
                    result &= _predictionList[i].Invoke();
                }
            }
            return result;
        }

        void IState.StateRemove()
        {
            GameInstance.Instance.GetGameModule<UnitStateModule>().ReturnState(this);
            ExtendedStateComplite();
        }
        #endregion
        /// <summary>
        /// Настройка всех условий выполнения для текущего состояния.
        /// </summary>
        private void SetupStatePrediction()
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = this.GetType().GetMethods(flag);
            for (int i = 0; i < methods.Length; ++i)
            {
                var attr = methods[i].GetCustomAttributes(true);
                for (int j = 0; j < attr.Length; ++j)
                {
                    if (attr[j] is StatePredictionAttribute && methods[i].ReturnType == typeof(bool))
                    {
                        var predictate = attr[j] as StatePredictionAttribute;
                        _predictionList.Add(predictate.CreateStatePrediciton(this, methods[i]));
                    }
                }
            }
        }
        /// <summary>
        /// Настройка подключения к игровой логике.
        /// </summary>
        private void SetupHandler()
        {
            var attribute = GetType().GetCustomAttribute<RequiredHandlerAttribute>();
            if(attribute is null)
            {
                _handlerType = typeof(NullOrErrorHandler);
            }
            else
            {
                _handlerType = attribute.HandlerType;
            }
        }
    }
}
