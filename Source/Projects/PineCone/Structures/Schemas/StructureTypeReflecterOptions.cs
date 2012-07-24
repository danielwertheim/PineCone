using System;

namespace PineCone.Structures.Schemas
{
    [Serializable]
    public class StructureTypeReflecterOptions
    {
        public bool IncludeNestedStructureMembers { get; set; }

        public StructureTypeReflecterOptions()
        {
            IncludeNestedStructureMembers = false;
        }
    }
}