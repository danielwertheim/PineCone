﻿using System;
using PineCone.Structures.Schemas.Configuration;

namespace PineCone.Structures.Schemas
{
    public class StructureTypeFactory : IStructureTypeFactory
    {
        public Func<IStructureTypeConfig, IStructureTypeReflecter> ReflecterFn { get; set; }

        public IStructureTypeConfigurations Configurations { get; set; }

        public StructureTypeFactory(Func<IStructureTypeConfig, IStructureTypeReflecter> reflecterFn = null, IStructureTypeConfigurations configurations = null)
        {
            ReflecterFn = reflecterFn ?? (cfg => new StructureTypeReflecter());
            Configurations = configurations ?? new StructureTypeConfigurations();
        }

        public virtual IStructureType CreateFor<T>() where T : class 
        {
            return CreateFor(typeof(T));
        }

        public virtual IStructureType CreateFor(Type structureType)
        {
            var config = Configurations.GetConfiguration(structureType);
            var reflecter = ReflecterFn(config);
            var shouldIndexAllMembers = config.IndexConfigIsEmpty;

            if (shouldIndexAllMembers)
                return new StructureType(
                    structureType,
                    reflecter.GetIdProperty(structureType),
                    reflecter.GetConcurrencyTokenProperty(structureType),
                    reflecter.GetTimeStampProperty(structureType),
                    reflecter.GetIndexableProperties(structureType, config.IncludeNestedStructureMembers),
                    reflecter.GetContainedStructureProperties(structureType));

            var shouldIndexAllMembersExcept = config.MemberPathsNotBeingIndexed.Count > 0;
            return new StructureType(
                structureType,
                reflecter.GetIdProperty(structureType),
                reflecter.GetConcurrencyTokenProperty(structureType),
                reflecter.GetTimeStampProperty(structureType),
                (shouldIndexAllMembersExcept
                    ? reflecter.GetIndexablePropertiesExcept(structureType, config.IncludeNestedStructureMembers, config.MemberPathsNotBeingIndexed)
                    : reflecter.GetSpecificIndexableProperties(structureType, config.IncludeNestedStructureMembers, config.MemberPathsBeingIndexed)),
                reflecter.GetContainedStructureProperties(structureType));
        }
    }
}