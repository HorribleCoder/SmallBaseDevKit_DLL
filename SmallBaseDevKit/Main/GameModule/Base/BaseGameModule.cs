﻿namespace SmallBaseDevKit.GameModule
{
    /// <summary>
    /// Базовый класс для всех игровых модулей. Обращение к реализации модуля через 
    /// <see cref="GameInstance"/>.
    /// </summary>
    public abstract class BaseGameModule 
    {
        public BaseGameModule()
        {
            CreateModule();
        }
        protected abstract void CreateModule();

        public virtual void DebugModule()
        {
            _Debug.Log($"Module - {GetType().Name} on debug!", DebugColor.orange);
        }
    }
}
