﻿using System;

namespace PineCone.Structures.Schemas.MemberAccessors
{
    public abstract class MemberAccessorBase : IMemberAccessor
    {
        protected IStructureProperty Property { get; private set; }

        public string Path
        {
            get { return Property.Path; }
        }

        public Type DataType
        {
            get { return Property.DataType; }
        }

        public DataTypeCode DataTypeCode
        {
            get { return Property.DataTypeCode; }
        }

        protected MemberAccessorBase(IStructureProperty property)
        {
            Property = property;
        }
    }
}