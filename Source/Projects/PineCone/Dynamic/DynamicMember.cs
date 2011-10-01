using System;

namespace PineCone.Dynamic
{
    [Serializable]
    public class DynamicMember
    {
        public string Name { get; private set; }

        public Type Type { get; private set; }

        public DynamicMember(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}