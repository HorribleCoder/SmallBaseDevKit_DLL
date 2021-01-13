using System;

namespace SmallBaseDevKit.USH.State
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredHandlerAttribute : System.Attribute
    {
        public Type HandlerType { get; set; }

        public RequiredHandlerAttribute(Type handlerType)
        {
            this.HandlerType = handlerType;
        }
    }
}
