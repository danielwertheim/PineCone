using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace PineCone.Dynamic
{
    [Serializable]
    public class DynamicStructure : DynamicObject, IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _memberStates;

        public DynamicDescriptor Descriptor { get; private set; }

        public Guid StructureId { get; set; }

        public string Name
        {
            get
            {
                return Descriptor.Name;
            }
        }

        public DynamicStructure(string name)
        {
            Descriptor = new DynamicDescriptor(name);

            _memberStates = new Dictionary<string, object>();
        }

        public DynamicStructure(string name, IEnumerable<KeyValuePair<string, object>> memberStates)
            : this(name)
        {
            VisitState(this, memberStates);
        }

        private static void VisitState(DynamicStructure ds, IEnumerable<KeyValuePair<string, object>> memberStates)
        {
            foreach (var member in memberStates)
                ds.SetValue(member.Key, member.Value);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _memberStates.Keys;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetValue(binder.Name, value);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _memberStates.TryGetValue(binder.Name, out result);
        }

        private void SetValue<T>(string member, T value)
        {
            _memberStates[member] = value;
            Descriptor.Add(member, value.GetType());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _memberStates.GetEnumerator();
        }

        public Dictionary<string, object> ToDictionary()
        {
            return _memberStates;
        }

        public static implicit operator Dictionary<string, object>(DynamicStructure ds)
        {
            return ds.ToDictionary();
        }
    }
}