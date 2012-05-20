using System;
using System.Collections.Generic;
using System.Reflection;
using PineCone.Structures.Schemas;

namespace PineCone.Tests.UnitTests.Structures.Schemas.StructureTypeReflecterTests
{
    internal class TestableReflecter : StructureTypeReflecter
    {
        internal virtual IEnumerable<PropertyInfo> InvokeGetSimpleIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            return GetSimpleIndexablePropertyInfos(properties, parent, nonIndexablePaths, indexablePaths);
        }

        internal virtual IEnumerable<PropertyInfo> InvokeGetComplexIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            return GetComplexIndexablePropertyInfos(properties, parent, nonIndexablePaths, indexablePaths);
        }

        internal virtual IEnumerable<PropertyInfo> InvokeGetEnumerableIndexablePropertyInfos(PropertyInfo[] properties, IStructureProperty parent = null, ICollection<string> nonIndexablePaths = null, ICollection<string> indexablePaths = null)
        {
            return GetEnumerableIndexablePropertyInfos(properties, parent, nonIndexablePaths, indexablePaths);
        }
    }
}