// <copyright file="ClassFactory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;

    /// <summary>
    /// Class factory.
    /// </summary>
    internal class ClassFactory
    {
        private static readonly Lazy<ClassFactory> InstanceLazy = new Lazy<ClassFactory>(() => new ClassFactory());

        private readonly ModuleBuilder module;
        private readonly Dictionary<Signature, Type> classes;
        private readonly ReaderWriterLock rwLock;
        private int classCount;

        private ClassFactory()
        {
            AssemblyName name = new AssemblyName("DynamicClasses");
            AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
            this.module = assembly.DefineDynamicModule("Module");
            this.classes = new Dictionary<Signature, Type>();
            this.rwLock = new ReaderWriterLock();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ClassFactory Instance => InstanceLazy.Value;

        /// <summary>
        /// Gets dynamic class with specified dynamic properties.
        /// </summary>
        /// <param name="properties">Properties for the resultant type.</param>
        /// <returns>Type with specified properties.</returns>
        public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
        {
            this.rwLock.AcquireReaderLock(Timeout.Infinite);
            try
            {
                Signature signature = new Signature(properties);
                if (!this.classes.TryGetValue(signature, out Type type))
                {
                    type = this.CreateAndCacheDynamicClass(signature);
                }

                return type;
            }
            finally
            {
                this.rwLock.ReleaseReaderLock();
            }
        }

        private Type CreateAndCacheDynamicClass(Signature signature)
        {
            LockCookie cookie = this.rwLock.UpgradeToWriterLock(Timeout.Infinite);
            try
            {
                if (!this.classes.TryGetValue(signature, out Type type))
                {
                    type = this.CreateDynamicClass(signature.Properties);
                    this.classes.Add(signature, type);
                }

                return type;
            }
            finally
            {
                this.rwLock.DowngradeFromWriterLock(ref cookie);
            }
        }

        private Type CreateDynamicClass(DynamicProperty[] properties)
        {
            string typeName = "DynamicClass" + (this.classCount + 1);
            TypeBuilder tb = this.module.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(DynamicClass));
            FieldInfo[] fields = this.GenerateProperties(tb, properties);
            this.GenerateEquals(tb, fields);
            this.GenerateGetHashCode(tb, fields);
            Type result = tb.CreateTypeInfo();
            this.classCount++;
            return result;
        }

        private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
        {
            FieldInfo[] fields = new FieldBuilder[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                DynamicProperty dp = properties[i];
                FieldBuilder fb = tb.DefineField("_" + dp.Name, dp.Type, FieldAttributes.Private);
                PropertyBuilder pb = tb.DefineProperty(dp.Name, PropertyAttributes.HasDefault, dp.Type, null);
                MethodBuilder mbGet = tb.DefineMethod("get_" + dp.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, dp.Type, Type.EmptyTypes);
                ILGenerator genGet = mbGet.GetILGenerator();
                genGet.Emit(OpCodes.Ldarg_0);
                genGet.Emit(OpCodes.Ldfld, fb);
                genGet.Emit(OpCodes.Ret);
                MethodBuilder mbSet = tb.DefineMethod("set_" + dp.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { dp.Type });
                ILGenerator genSet = mbSet.GetILGenerator();
                genSet.Emit(OpCodes.Ldarg_0);
                genSet.Emit(OpCodes.Ldarg_1);
                genSet.Emit(OpCodes.Stfld, fb);
                genSet.Emit(OpCodes.Ret);
                pb.SetGetMethod(mbGet);
                pb.SetSetMethod(mbSet);
                fields[i] = fb;
            }

            return fields;
        }

        private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
        {
            MethodBuilder mb = tb.DefineMethod(nameof(object.Equals), MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new[] { typeof(object) });
            ILGenerator gen = mb.GetILGenerator();
            LocalBuilder other = gen.DeclareLocal(tb);
            Label next = gen.DefineLabel();
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Isinst, tb);
            gen.Emit(OpCodes.Stloc, other);
            gen.Emit(OpCodes.Ldloc, other);
            gen.Emit(OpCodes.Brtrue_S, next);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ret);
            gen.MarkLabel(next);
            foreach (FieldInfo field in fields)
            {
                Type ft = field.FieldType;
                Type ct = typeof(EqualityComparer<>).MakeGenericType(ft);
                next = gen.DefineLabel();
                gen.EmitCall(OpCodes.Call, ct.GetMethod("get_Default"), null);
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
                gen.Emit(OpCodes.Ldloc, other);
                gen.Emit(OpCodes.Ldfld, field);
                gen.EmitCall(OpCodes.Callvirt, ct.GetMethod(nameof(object.Equals), new[] { ft, ft }), null);
                gen.Emit(OpCodes.Brtrue_S, next);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ret);
                gen.MarkLabel(next);
            }

            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Ret);
        }

        private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
        {
            MethodBuilder mb = tb.DefineMethod(nameof(object.GetHashCode), MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), Type.EmptyTypes);
            ILGenerator gen = mb.GetILGenerator();
            gen.Emit(OpCodes.Ldc_I4_0);
            foreach (FieldInfo field in fields)
            {
                Type ft = field.FieldType;
                Type ct = typeof(EqualityComparer<>).MakeGenericType(ft);
                gen.EmitCall(OpCodes.Call, ct.GetMethod("get_Default"), null);
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
                gen.EmitCall(OpCodes.Callvirt, ct.GetMethod(nameof(object.GetHashCode), new[] { ft }), null);
                gen.Emit(OpCodes.Xor);
            }

            gen.Emit(OpCodes.Ret);
        }
    }
}
