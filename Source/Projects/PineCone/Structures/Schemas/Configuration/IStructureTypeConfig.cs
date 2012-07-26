﻿using System;
using System.Collections.Generic;

namespace PineCone.Structures.Schemas.Configuration
{
    public interface IStructureTypeConfig
    {
        Type Type { get; }
        bool IndexConfigIsEmpty { get; }
        bool IncludeNestedStructureMembers { get; set; }
        ISet<string> MemberPathsBeingIndexed { get; }
        ISet<string> MemberPathsNotBeingIndexed { get; }
    }
}