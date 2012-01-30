﻿using System;

namespace PineCone.Structures.Schemas
{
    public interface IStructureType
    {
    	Type Type { get; } 
        string Name { get; }
        IStructureProperty IdProperty { get; }
		IStructureProperty[] IndexableProperties { get; }
    }
}