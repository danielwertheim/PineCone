using System;
using System.Collections.Generic;
using System.Reflection;

namespace PineCone.Structures.Schemas
{
    public interface IStructureTypeReflecter
    {
		bool HasIdProperty(Type type);

        IStructureProperty GetIdProperty(Type type);

        IEnumerable<IStructureProperty> GetIndexableProperties(IReflect type);

        IEnumerable<IStructureProperty> GetIndexablePropertiesExcept(IReflect type, ICollection<string> nonIndexablePaths);

        IEnumerable<IStructureProperty> GetSpecificIndexableProperties(IReflect type, ICollection<string> indexablePaths);
    }
}