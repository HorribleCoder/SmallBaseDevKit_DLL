using UnityEngine;

using SmallBaseDevKit.Pool;
using SmallBaseDevKit.GameModule;

namespace SmallBaseDevKit.Main
{
    /// <summary>
    /// Класс-инициализатор в котором хранятся все необходимые игровые модули.
    /// </summary>
    public sealed class GameInstance : MonoSingleton<GameInstance>
    {
        private IPool<BaseGameModule> _modulePool;

        [RuntimeInitializeOnLoadMethod]
        public static void InitialazeGame()
        {
            var self = GameInstance.Instance;
        }
        /// <summary>
        /// Метод получения конктреного модуля.
        /// </summary>
        /// <typeparam name="T">Тип моудля.</typeparam>
        /// <returns>Объект модуля.</returns>
        public T GetGameModule<T>() where T : BaseGameModule
        {
            if(_modulePool is null)
            {
                _modulePool = new LinePool<BaseGameModule>();
            }
            var module = _modulePool.GetObject(typeof(T)) as T;
            // обязательно возвращаем моудль в пул
            _modulePool.ReturnObject(module);
            return module;
        }

        public void TestDebugUnitModule()
        {
            GetGameModule<UnitModule>().DebugModule();
        }

        public void TestDebugStateModule()
        {
            GetGameModule<UnitStateModule>().DebugModule();
        }
    }
}
