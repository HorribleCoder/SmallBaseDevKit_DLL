using System;
using System.Collections.Generic;
using UnityEngine;

using SmallBaseDevKit.GameModule;

namespace SmallBaseDevKit.USH.Unit
{
    /// <summary>
    /// Класс по хранению и работе с Unity компонентами, что использует игровая единица.
    /// <para>Может подключать  нужные компоненты в момент создания игровой единицы, через <see cref="RequireComponent"/></para>
    /// <para>По умолчанию собирает все компоненты, включая дочерние компоненты, из целевого GameObject.т</para>
    /// </summary>
    public sealed class ComponentHandler
    {
        public GameObject UnitVisual { get => _unitVisual; }
        private GameObject _unitVisual;
        private List<Component> _componentList;

        internal ComponentHandler()
        {
            _componentList = new List<Component>();
        }
        /// <summary>
        /// Метод настройки класса с Unity компонентамию. Может обрабатывать атрибут <see cref="RequireComponent"/>, что использует класс инициализатор.
        /// <para>Весьма медленный метод, использовать с умом.</para>
        /// </summary>
        /// <typeparam name="T">Тип игровой единицы.</typeparam>
        /// <param name="unit"></param>
        /// <param name="targetObject"></param>
        public void SetupComponentHandler<T>(T unit, GameObject targetObject) where T: IUnit
        {
            #region Local Function
            bool CheckIsComponet(Type checkType)
            {
                var result = false;
                if(checkType != null)
                {
                    result = checkType.IsSubclassOf(typeof(Component));
                }
                return result;
            }
            bool TryGetRequireComponent(out List<Type> list)
            {
                var attributes = unit.GetType().GetCustomAttributes(typeof(RequireComponent), true);
                list = new List<Type>();
                for(int i = 0; i < attributes.Length; ++i)
                {
                    var requierAttribute = attributes[i] as RequireComponent;
                    if (CheckIsComponet(requierAttribute.m_Type0))
                    {
                        list.Add(requierAttribute.m_Type0);
                    }
                    if(CheckIsComponet(requierAttribute.m_Type1))
                    {
                        list.Add(requierAttribute.m_Type1);
                    }
                    if (CheckIsComponet(requierAttribute.m_Type2))
                    {
                        list.Add(requierAttribute.m_Type2);
                    }
                }
                return list.Count > 0;
            }
            #endregion
            try
            {
                if(targetObject is null)
                {
                    throw new Exception();
                }
                var targetComponentList = targetObject.GetComponentsInChildren<Component>();
                _unitVisual = targetObject;
                if (TryGetRequireComponent(out var r_list))
                {
                    for(int i = 0; i < targetComponentList.Length; ++i)
                    {
                        if (r_list.Contains(targetComponentList[i].GetType()))
                        {
                            _componentList.Add(targetComponentList[i]);
                            r_list.Remove(targetComponentList[i].GetType());
                        }
                        if(r_list.Count <= 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    _componentList.AddRange(targetComponentList);
                }
                
            }
            catch(Exception e)
            {
                GameException.ExceptionHandler.ExceptionProcessExecute(e, $"Null target object (GameObject) in <b>SetupComponentHandler()</b> by unit - {unit.GetType().Name}");
            }
           
        }

        public T GetComponent<T>() where T : Component
        {
            return _componentList.Find(x => x.GetType() == typeof(T)) as T;
        }

        public void RemoveComponent<T>() where T : Component
        {
            var findComponent = _componentList.Find(x => x.GetType() == typeof(T));
            _componentList.Remove(findComponent);
        }

        public void ResetComponentHandler()
        {
            _componentList.Clear();
            _unitVisual = null;
        }
        //TODD Remove this!
        //test
        public void DebugView()
        {
            for(int i = 0; i< _componentList.Count; ++i)
            {
                _Debug.Log($"ComponentHandler - {_componentList[i]}");
            }
        }
    }
}
