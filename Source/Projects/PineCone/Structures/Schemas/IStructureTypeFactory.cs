using System;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Structures.Schemas
{
    public interface IStructureTypeFactory
    {
        Func<IStructureTypeConfig, IStructureTypeReflecter> ReflecterFn { get; set; }

        IStructureTypeConfigurations Configurations { get; set; }

        IStructureType CreateFor<T>() where T : class;
        IStructureType CreateFor(Type type);
    }
}