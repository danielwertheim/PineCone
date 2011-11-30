using System;
using System.Reflection;
using System.Reflection.Emit;
using NCore.Reflections;

namespace PineCone.Structures.Schemas
{
    public static class DynamicPropertyFactory
    {
        private static readonly Type ObjectType = typeof(object);
        private static readonly Type VoidType = typeof(void);
        private static readonly Type DynamicGetterType = typeof(DynamicGetter);
        private static readonly Type DynamicSetterType = typeof(DynamicSetter);

        public static DynamicProperty Create(PropertyInfo propertyInfo)
        {
            if (propertyInfo.DeclaringType.IsKeyValuePairType())
                return new DynamicProperty(propertyInfo, (o) => propertyInfo.GetValue(o, null), (o, v) => propertyInfo.SetValue(o, v, null));

            return new DynamicProperty(propertyInfo, CreateGetter(propertyInfo), CreateSetter(propertyInfo));
        }

        private static DynamicGetter CreateGetter(PropertyInfo propertyInfo)
        {
            var propGetMethod = propertyInfo.GetGetMethod(true);
            if (propGetMethod == null)
                return null;

            var getter = CreateDynamicMethod(propertyInfo, isForGetter: true);

            var generator = getter.GetILGenerator();
            generator.DeclareLocal(ObjectType);
            generator.Emit(OpCodes.Ldarg_0);
            //generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.EmitCall(OpCodes.Callvirt, propGetMethod, null);

            if (!propertyInfo.PropertyType.IsClass)
                generator.Emit(OpCodes.Box, propertyInfo.PropertyType);

            generator.Emit(OpCodes.Ret);

            return (DynamicGetter)getter.CreateDelegate(DynamicGetterType);
        }

        private static DynamicSetter CreateSetter(PropertyInfo propertyInfo)
        {
            var propSetMethod = propertyInfo.GetSetMethod(true);
            if (propSetMethod == null)
                return null;

            var setter = CreateDynamicMethod(propertyInfo, isForGetter: false);

            var generator = setter.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            //generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.Emit(OpCodes.Ldarg_1);

            generator.Emit(propertyInfo.PropertyType.IsClass ? OpCodes.Castclass : OpCodes.Unbox_Any,
                           propertyInfo.PropertyType);

            generator.EmitCall(OpCodes.Callvirt, propSetMethod, null);
            generator.Emit(OpCodes.Ret);

            return (DynamicSetter)setter.CreateDelegate(DynamicSetterType);
        }

        private static DynamicMethod CreateDynamicMethod(PropertyInfo propertyInfo, bool isForGetter)
        {
            var args = isForGetter ? new[] { ObjectType } : new[] { ObjectType, ObjectType };
            var name = string.Format("_{0}{1}_", isForGetter ? "Get" : "Set", propertyInfo.Name);
            var returnType = isForGetter ? ObjectType : VoidType;

            return !propertyInfo.DeclaringType.IsInterface
                ? new DynamicMethod(
                    name,
                    returnType,
                    args,
                    propertyInfo.DeclaringType,
                    true)
                : new DynamicMethod(
                    name,
                    returnType,
                    args,
                    propertyInfo.Module,
                    true);
        }
    }
}