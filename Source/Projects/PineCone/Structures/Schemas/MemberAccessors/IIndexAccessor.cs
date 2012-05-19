﻿using System;
using System.Collections.Generic;
using PineCone.Annotations;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public interface IIndexAccessor : IMemberAccessor
    {
        bool IsEnumerable { get; }
        bool IsElement { get; }
        bool IsUnique { get; }
        Type ElementDataType { get; }
        DataTypeCode? ElementDataTypeCode { get; }
        UniqueMode? UniqueMode { get; }
        
        IList<object> GetValues<T>(T item) where T : class;
        void SetValue<T>(T item, object value) where T : class;
    }
}