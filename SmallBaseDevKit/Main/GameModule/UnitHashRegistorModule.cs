using System;
using System.Collections.Generic;

using SmallBaseDevKit.USH.Unit;
using SmallBaseDevKit.GameException;

namespace SmallBaseDevKit.GameModule
{
    internal sealed class UnitHashRegistorModule : BaseGameModule
    {
        private IDictionary<Type, IDictionary<int, IUnit>> _unitHashRegistor;
        protected override void CreateModule()
        {
            _unitHashRegistor = new Dictionary<Type, IDictionary<int, IUnit>>();
        }

        internal void RegistrationUnit<T>(T type, IUnit unit) where T : class
        {
            if (!_unitHashRegistor.TryGetValue(type.GetType(), out var currentDictionary))
            {
                currentDictionary = new Dictionary<int, IUnit>();
                _unitHashRegistor.Add(type.GetType(), currentDictionary);
            }
            if (!currentDictionary.ContainsKey(type.GetHashCode()))
            {
                currentDictionary.Add(type.GetHashCode(), unit);
            }
        }

        internal void UnregistrationUnit<T>(T type, IUnit unit) where T : class
        {
            if(_unitHashRegistor.TryGetValue(type.GetType(), out var currentDictionary))
            {
                currentDictionary.Remove(type.GetHashCode());
            }
        }

        internal IUnit GetUnitInRegistor<T>(T unitKey) where T : class
        {
            IUnit unit = default;
            try
            {
                if(_unitHashRegistor.TryGetValue(typeof(T), out var currentDictionary))
                {
                    currentDictionary.TryGetValue(unitKey.GetHashCode(), out unit);
                }
                if(unit == default)
                {
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                ExceptionHandler.ExceptionProcessExecute(e, $"Unit Registor don't have Unit with hash-key {typeof(T).Name}");
            }
            return unit;
        }

        internal int GetUnitIndexInRegistor<T>(IUnit unit) where T : class
        {
            int unitIndex = 0;
            try
            {
                if(!_unitHashRegistor.TryGetValue(typeof(T), out var currentDictionary))
                {
                    throw new Exception();
                }
                else
                {
                    bool haveResult = false;
                    foreach(var el in currentDictionary)
                    {
                        if(el.Value.GetHashCode() == unit.GetHashCode())
                        {
                            haveResult = true;
                            break;
                        }
                        unitIndex++;
                    }
                    if (!haveResult)
                    {
                        throw new Exception();
                    }
                }
                
            }
            catch(Exception e)
            {
                ExceptionHandler.ExceptionProcessExecute(e, $"Unit {unit.GetType().Name} don't registred in list by key - {typeof(T)}");
            }

            return unitIndex;
        }

        internal int GetUnitCountInRegistorByType<T>() where T : class
        {
            int result = 0;
            if(_unitHashRegistor.TryGetValue(typeof(T), out var currentDictionary))
            {
                result = currentDictionary.Count;
            }
            return result;
        }

        internal void ResetUnitRegistor()
        {
            _unitHashRegistor.Clear();
        }
    }
}
