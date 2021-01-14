using System;
using System.Reflection;

namespace SmallBaseDevKit.USH.State
{
    /// <summary>
    /// Атрибут для назначения условия выполнения "состояния".
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class StatePredictionAttribute : System.Attribute
    {
        /// <summary>
        /// Создаем делегат функции.
        /// </summary>
        /// <param name="target">Объект применения делегата.</param>
        /// <param name="info">Информация о методе.</param>
        /// <returns>Делегат на метод.</returns>
        public Func<bool> CreateStatePrediciton(object target, MethodInfo info)
        {
            return (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), target, info);
        }
    }
}
