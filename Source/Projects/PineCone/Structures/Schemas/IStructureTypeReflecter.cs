﻿using System;
using System.Collections.Generic;

namespace PineCone.Structures.Schemas
{
    /// <summary>
    /// Responsible for identifying the Properties that should be used as
    /// StructureId, TimeStamp, ConcurrencyToken and plain indexes for a
    /// certain structure type.
    /// </summary>
    public interface IStructureTypeReflecter
    {
        Type StructureType { get; }
        IStructurePropertyFactory PropertyFactory { set; }

        bool HasIdProperty();
        bool HasConcurrencyTokenProperty();
        bool HasTimeStampProperty();

        IStructureProperty GetIdProperty();
        IStructureProperty GetConcurrencyTokenProperty();
        IStructureProperty GetTimeStampProperty();
        
        IStructureProperty[] GetIndexableProperties();
		IStructureProperty[] GetIndexablePropertiesExcept(ICollection<string> nonIndexablePaths);
        IStructureProperty[] GetSpecificIndexableProperties(ICollection<string> indexablePaths);
    }
}