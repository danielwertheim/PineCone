using System;
using System.Linq;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Structures.Schemas
{
    public class StructureTypeFactory : IStructureTypeFactory
    {
        public IStructureTypeReflecter Reflecter { get; private set; }

        public IStructureTypeConfigurations Configurations { get; private set; }

        public StructureTypeFactory(IStructureTypeReflecter reflecter = null, IStructureTypeConfigurations configurations = null)
        {
            Reflecter = reflecter ?? new StructureTypeReflecter();
            Configurations = configurations ?? new StructureTypeConfigurations();
        }

        public IStructureType CreateFor<T>() where T : class 
        {
            return CreateFor(TypeFor<T>.Type);
        }

        public IStructureType CreateFor(Type type)
        {
            var config = Configurations.GetConfiguration(type);

            //Scenario: Index ALL which is the default behavior
            if (config == null || config.IsEmpty)
                return new StructureType(
                    type.Name,
                    Reflecter.GetIdProperty(type),
                    Reflecter.GetIndexableProperties(type).ToArray());

            return new StructureType(
                type.Name,
                Reflecter.GetIdProperty(type),
                ((config.MemberPathsNotBeingIndexed.Count > 0)
                ? Reflecter.GetIndexablePropertiesExcept(type, config.MemberPathsNotBeingIndexed) //Scenario: Index ALL EXCEPT
                : Reflecter.GetSpecificIndexableProperties(type, config.MemberPathsBeingIndexed)).ToArray());//Scenario: Index only THIS
        }
    }
}