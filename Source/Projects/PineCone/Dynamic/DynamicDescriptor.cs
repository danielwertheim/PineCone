using System;
using System.Collections;
using System.Collections.Generic;
using NCore.Validation;

namespace PineCone.Dynamic
{
    [Serializable]
    public class DynamicDescriptor : IEnumerable<DynamicMember>
    {
        private readonly Dictionary<string, DynamicMember> _state;

        public string Name { get; private set; }

        public DynamicDescriptor(string name)
        {
            Ensure.Param(name, "name").HasNonWhiteSpaceValue();
            Name = name;

            _state = new Dictionary<string, DynamicMember>();
        }

        public DynamicMember Get(string name)
        {
            return _state.ContainsKey(name) ? _state[name] : null;
        }

        public void Add(string name, Type type)
        {
            _state[name] = new DynamicMember(name, type);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<DynamicMember> GetEnumerator()
        {
            return _state.Values.GetEnumerator();
        }
    }
}