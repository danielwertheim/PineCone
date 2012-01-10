using System;
using System.Collections.Generic;

namespace PineCone.Structures.Schemas
{
    public interface IStructureTypeReflecter
    {
		bool HasIdProperty(Type type);

        IStructureProperty GetIdProperty(Type type);

		IStructureProperty[] GetIndexableProperties(Type type);
						  
		IStructureProperty[] GetIndexablePropertiesExcept(Type type, ICollection<string> nonIndexablePaths);
						  
		IStructureProperty[] GetSpecificIndexableProperties(Type type, ICollection<string> indexablePaths);
    }
}