using System;

namespace PineCone.Structures
{
    [Serializable]
    public enum StructureIdTypes
    {
        Guid,
        Identity,
        BigIdentity,
        String
    }

    public static class StructureIdTypesExtensions
    {
		public static bool IsGuid(this StructureIdTypes structureIdType)
		{
			return structureIdType == StructureIdTypes.Guid;
		}

        public static bool IsIdentity(this StructureIdTypes structureIdType)
        {
            return structureIdType == StructureIdTypes.Identity || structureIdType == StructureIdTypes.BigIdentity;
        }
    }
}