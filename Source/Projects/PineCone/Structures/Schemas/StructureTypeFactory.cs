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

        public virtual IStructureType CreateFor<T>() where T : class 
        {
            return CreateFor(typeof(T));
        }

        public virtual IStructureType CreateFor(Type type)
        {
            var config = Configurations.GetConfiguration(type);
            var shouldIndexAllMembers = config == null || config.IsEmpty;
            
            if (shouldIndexAllMembers)
                return new StructureType(
                    type,
                    Reflecter.GetIdProperty(type),
                    Reflecter.GetConcurrencyTokenProperty(type),
                    Reflecter.GetTimeStampProperty(type),
                    Reflecter.GetIndexableProperties(type).ToArray());

            var shouldIndexAllMembersExcept = config.MemberPathsNotBeingIndexed.Count > 0;
            return new StructureType(
                type,
                Reflecter.GetIdProperty(type),
                Reflecter.GetConcurrencyTokenProperty(type),
                Reflecter.GetTimeStampProperty(type),
                (shouldIndexAllMembersExcept
                    ? Reflecter.GetIndexablePropertiesExcept(type, config.MemberPathsNotBeingIndexed)
                    : Reflecter.GetSpecificIndexableProperties(type, config.MemberPathsBeingIndexed)).ToArray());
        }
    }
}