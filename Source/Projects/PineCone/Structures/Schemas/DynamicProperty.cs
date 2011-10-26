using System.Reflection;
using EnsureThat;

namespace PineCone.Structures.Schemas
{
    public class DynamicProperty
    {
        public readonly PropertyInfo PropertyInfo;
        public readonly DynamicGetter Getter;
        public readonly DynamicSetter Setter;
        public readonly bool IsReadOnly;

        public DynamicProperty(PropertyInfo propertyInfo, DynamicGetter getter, DynamicSetter setter = null)
        {
            Ensure.That(propertyInfo, "propertyInfo").IsNotNull();
            Ensure.That(getter, "getter").IsNotNull();

            PropertyInfo = propertyInfo;
            Getter = getter;
            Setter = setter;
            IsReadOnly = Setter == null;
        }
    }
}