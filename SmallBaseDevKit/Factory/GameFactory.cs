using System;
using UnityEngine;

namespace SmallBaseDevKit.Factory
{
    internal sealed class GameFactory : IFactory
    {
        public static GameFactory Instance
        {
            get
            {
                if(_instance is null)
                {
                    _instance = new GameFactory();
                }
                return _instance;
            }
        }
        private static GameFactory _instance;

        private GameFactory()
        {

        }

        public T CreateNewObject<T>(object prototype) where T : class
        {
            T newObject = default;
            if (typeof(T).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                var prt = (UnityEngine.Object)prototype;
                var go = MonoBehaviour.Instantiate(prt);
                go.name = prt.name;
                newObject = go as T;
            }
            else
            {
                newObject = Activator.CreateInstance((Type)prototype) as T;
            }
            return newObject;
        }
    }
}
