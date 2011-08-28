using System;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Structures.Schemas
{
    public interface IStructureTypeFactory
    {
        IStructureTypeReflecter Reflecter { get; set; }

        IStructureTypeConfigurations Configurations { get; set; }

        IStructureType CreateFor<T>() where T : class;

        IStructureType CreateFor(Type type);
    }
}