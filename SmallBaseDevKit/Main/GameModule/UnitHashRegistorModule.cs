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

        internal void RegistrationUnit<RegistrationType>(RegistrationType type, IUnit unit) where RegistrationType : class
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

        internal void UnregistrationUnit<UnregistrationType>(UnregistrationType type, IUnit unit) where UnregistrationType : class
        {
            if(_unitHashRegistor.TryGetValue(type.GetType(), out var currentDictionary))
            {
                currentDictionary.Remove(type.GetHashCode());
            }
        }

        internal IUnit GetUnitInRegistor<TypeKey>(TypeKey unitKey) where TypeKey : class
        {
            IUnit unit = default;
            try
            {
                if(_unitHashRegistor.TryGetValue(typeof(TypeKey), out var currentDictionary))
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
                ExceptionHandler.ExceptionProcessExecute(e, $"Unit Registor don't have Unit with hash-key {typeof(TypeKey).Name}");
            }
            return unit;
        }
    }
}
