using System;
using System.Collections.Generic;

namespace PineCone.Structures
{
    public interface IStructure
    {
        Guid Id { get; }

        string Name { get; }

        string Data { get; set; }
        
        IList<IStructureIndex> Indexes { get; }

        IList<IStructureIndex> Uniques { get; }
    }
}