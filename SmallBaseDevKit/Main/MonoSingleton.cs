using UnityEngine;

namespace SmallBaseDevKit
{
    /// <summary>
    /// Класс одиночка для игровых объектов.
    /// </summary>
    /// <typeparam name="T">Наследник <see cref="MonoBehaviour"/></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if(_instance is null)
                {
                    var go = new GameObject($">>{typeof(T).Name}<<");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }

        private static T _instance;
    }
}
