using System;

namespace SmallBaseDevKit.USH.State
{
    /// <summary>
    /// Атрибут для подключения состояния юнита к обработчику.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class RequiredHandlerAttribute : System.Attribute
    {
        public Type HandlerType { get; set; }

        public RequiredHandlerAttribute(Type handlerType)
        {
            this.HandlerType = handlerType;
        }
    }
}
