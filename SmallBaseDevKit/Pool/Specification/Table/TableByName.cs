namespace SmallBaseDevKit.Pool.Specification
{
    internal sealed class TableByName<T> : BaseTablePoolSpecification<string, T>
        where T : class
    {
        protected internal override bool CheckInputPrototypeData(object objectPrototype)
        {
            if (objectPrototype is null)
            {
                return false;
            }
            return objectPrototype.GetType() == typeof(T);
        }

        protected internal override string GetPrototypeKey(object prototypeObject)
        {
            var prtGo = (UnityEngine.Object)prototypeObject;
            return prtGo.name;
        }
    }
}

