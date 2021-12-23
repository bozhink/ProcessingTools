// <copyright file="DynamicProxyBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Dynamic proxy builder.
    /// </summary>
    public static class DynamicProxyBuilder
    {
        /// <summary>
        /// Default instance of <see cref="System.Reflection.AssemblyName"/>.
        /// </summary>
        public static readonly AssemblyName AssemblyName = new AssemblyName("DynamicProxyAssembly");

        /// <summary>
        /// Default instance of <see cref="System.Reflection.Emit.AssemblyBuilder"/>.
        /// </summary>
        public static readonly AssemblyBuilder AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run);

        /// <summary>
        /// Default instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.
        /// </summary>
        public static readonly ModuleBuilder ModuleBuilder = AssemblyBuilder.DefineDynamicModule("DynamicProxyModule");

        private static readonly ConcurrentDictionary<Type, Type> DynamicProxyTypesCache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// Get from cache or create proxy type of specified type.
        /// </summary>
        /// <typeparam name="T">Type of interface to be implemented by the proxy type.</typeparam>
        /// <param name="moduleBuilder">Instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.</param>
        /// <returns>Generated proxy type that implements the specified interface.</returns>
        public static Type GetProxyTypeOf<T>(this ModuleBuilder moduleBuilder)
        {
            if (moduleBuilder is null)
            {
                throw new ArgumentNullException(nameof(moduleBuilder));
            }

            Type type = typeof(T);

            if (!type.IsInterface)
            {
                throw new InvalidOperationException();
            }

            return DynamicProxyTypesCache.GetOrAdd(type, t => moduleBuilder.CreateProxyTypeOf(t));
        }

        /// <summary>
        /// Get from cache or create proxy type of specified type.
        /// </summary>
        /// <param name="moduleBuilder">Instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.</param>
        /// <param name="type">Type of interface to be implemented by the proxy type.</param>
        /// <returns>Generated proxy type that implements the specified interface.</returns>
        public static Type GetProxyTypeOf(this ModuleBuilder moduleBuilder, Type type)
        {
            if (moduleBuilder is null)
            {
                throw new ArgumentNullException(nameof(moduleBuilder));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsInterface)
            {
                throw new InvalidOperationException();
            }

            return DynamicProxyTypesCache.GetOrAdd(type, t => moduleBuilder.CreateProxyTypeOf(t));
        }

        /// <summary>
        /// Create proxy type of specified type.
        /// </summary>
        /// <typeparam name="T">Type of interface to be implemented by the proxy type.</typeparam>
        /// <param name="moduleBuilder">Instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.</param>
        /// <returns>Generated proxy type that implements the specified interface.</returns>
        public static Type CreateProxyTypeOf<T>(this ModuleBuilder moduleBuilder)
        {
            return CreateProxyTypeOf(moduleBuilder, typeof(T));
        }

        /// <summary>
        /// Create proxy type of specified type.
        /// </summary>
        /// <param name="moduleBuilder">Instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.</param>
        /// <param name="type">Type of interface to be implemented by the proxy type.</param>
        /// <returns>Generated proxy type that implements the specified interface.</returns>
        public static Type CreateProxyTypeOf(this ModuleBuilder moduleBuilder, Type type)
        {
            if (moduleBuilder is null)
            {
                throw new ArgumentNullException(nameof(moduleBuilder));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsInterface)
            {
                throw new InvalidOperationException();
            }

            TypeBuilder typeBuilder = moduleBuilder.GetProxyTypeBuilder(type);

            typeBuilder.AddDefaultConstructor();

            var interfaces = type.GetInterfaces();

            // Properties are added as real getters and setters,
            // methods are added as dummy methods.
            var propertyInfos = type.GetProperties().Union(interfaces.SelectMany(i => i.GetProperties())).ToArray();

            var properties = propertyInfos.ToDictionary(p => p.Name, p => p.PropertyType);

            // Filter methods which names starts with get_ or set_ to not add as methods because they are added as properties.
            MethodInfo[] methods = type.GetMethods().Union(interfaces.SelectMany(i => i.GetMethods())).ToArray();
            var methodInfos = new List<MethodInfo>(methods.Length);

            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name.StartsWith("get_", StringComparison.InvariantCulture) && methodInfo.GetParameters().Length == 0)
                {
                    // This method is property getter.
                    properties.TryAdd(methodInfo.Name.Substring("get_".Length), methodInfo.ReturnType);
                }
                else if (methodInfo.Name.StartsWith("set_", StringComparison.InvariantCulture) && methodInfo.GetParameters().Length == 1)
                {
                    // This method is property setter.
                    properties.TryAdd(methodInfo.Name.Substring("set_".Length), methodInfo.ReturnType);
                }
                else
                {
                    // This does not look like property.
                    methodInfos.Add(methodInfo);
                }
            }

            methodInfos.TrimExcess();

            // Add properties.
            foreach (var property in properties)
            {
                typeBuilder.AddProperty(property.Key, property.Value, methods);
            }

            // Add methods.
            foreach (var methodInfo in methodInfos)
            {
                typeBuilder.AddDummyMethod(methodInfo);
            }

            return typeBuilder.CreateType();
        }

        /// <summary>
        /// Get instance of <see cref="System.Reflection.Emit.TypeBuilder"/> for proxy type.
        /// </summary>
        /// <param name="moduleBuilder">Instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.</param>
        /// <param name="type">Type of interface to be implemented by the proxy type.</param>
        /// <returns>Instance of <see cref="System.Reflection.Emit.TypeBuilder"/> for proxy type.</returns>
        public static TypeBuilder GetProxyTypeBuilder(this ModuleBuilder moduleBuilder, Type type)
        {
            if (moduleBuilder is null)
            {
                throw new ArgumentNullException(nameof(moduleBuilder));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsInterface)
            {
                throw new InvalidOperationException();
            }

            string suffix = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture).ToUpperInvariant();
            TypeBuilder typeBuilder = moduleBuilder.DefineType($"ProcessingTools.Extensions.Dynamic.DynamicProxy.{type.Name}_{suffix}", TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(type);
            return typeBuilder;
        }

        /// <summary>
        /// Add default constructor.
        /// </summary>
        /// <param name="typeBuilder">The builder of the type to be updated.</param>
        public static void AddDefaultConstructor(this TypeBuilder typeBuilder)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Array.Empty<Type>());
            ILGenerator ilgenerator = constructorBuilder.GetILGenerator();

#if DEBUG
            ilgenerator.EmitWriteLine("Creating Proxy instance");
#endif

            ilgenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Add dummy implementation for method.
        /// </summary>
        /// <param name="typeBuilder">The builder of the type to be updated.</param>
        /// <param name="methodInfo">Instance of <see cref="System.Reflection.MethodInfo"/> to be applied.</param>
        public static void AddDummyMethod(this TypeBuilder typeBuilder, MethodInfo methodInfo)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            if (methodInfo is null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            var parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            var returnType = methodInfo.ReturnType;
            var attributes = MethodAttributes.Public | MethodAttributes.Virtual;

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(name: methodInfo.Name, attributes: attributes, returnType: returnType, parameterTypes: parameterTypes);

            ILGenerator ilgenerator = methodBuilder.GetILGenerator();

            if (returnType == typeof(void))
            {
                ilgenerator.Emit(OpCodes.Ret);
            }
            else
            {
                if (returnType.IsValueType || returnType.IsEnum)
                {
                    Type typeOfType = typeof(Type);
                    MethodInfo methodGetTypeFromHandle = typeOfType.GetMethod(nameof(Type.GetTypeFromHandle));
                    MethodInfo methodCreateInstance = typeof(Activator).GetMethod(nameof(Activator.CreateInstance), new[] { typeOfType });

                    LocalBuilder localBuilder = ilgenerator.DeclareLocal(returnType);

                    ilgenerator.Emit(OpCodes.Ldtoken, localBuilder.LocalType);
                    ilgenerator.Emit(OpCodes.Call, methodGetTypeFromHandle);
                    ilgenerator.Emit(OpCodes.Callvirt, methodCreateInstance);
                    ilgenerator.Emit(OpCodes.Unbox_Any, localBuilder.LocalType);
                }
                else
                {
                    ilgenerator.Emit(OpCodes.Ldnull);
                }

                ilgenerator.Emit(OpCodes.Ret);
            }

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        /// <summary>
        /// Add property to type.
        /// </summary>
        /// <param name="typeBuilder">The builder of the type to be updated.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        public static void AddProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            string fieldName = $"__{propertyName}__";

            FieldBuilder fieldBuilder = typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Private);

            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use an array with no elements: new Type[] {})
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // The property set and property get methods require a special set of attributes.
            MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            MethodBuilder getMethodBuilder = typeBuilder.CreateGetMethodBuilder(propertyName, propertyType, fieldBuilder, methodAttributes);

            MethodBuilder setMethodBuilder = typeBuilder.CreateSetMethodBuilder(propertyName, propertyType, fieldBuilder, methodAttributes);

            // Last, we must map the two methods created above to our PropertyBuilder to
            // their corresponding behaviors, "get" and "set" respectively.
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);
        }

        /// <summary>
        /// Add property to type.
        /// </summary>
        /// <param name="typeBuilder">The builder of the type to be updated.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="methodInfos">List of <see cref="MethodInfo"/> for method override.</param>
        public static void AddProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType, IEnumerable<MethodInfo> methodInfos)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (methodInfos is null)
            {
                throw new ArgumentNullException(nameof(methodInfos));
            }

            string fieldName = $"__{propertyName}__";

            FieldBuilder fieldBuilder = typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Private);

            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use an array with no elements: new Type[] {})
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // The property set and property get methods require a special set of attributes.
            MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;

            MethodBuilder getMethodBuilder = typeBuilder.CreateGetMethodBuilder(propertyName, propertyType, fieldBuilder, methodAttributes);

            MethodBuilder setMethodBuilder = typeBuilder.CreateSetMethodBuilder(propertyName, propertyType, fieldBuilder, methodAttributes);

            // Last, we must map the two methods created above to our PropertyBuilder to
            // their corresponding behaviors, "get" and "set" respectively.
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            // Method overrides.
            MethodInfo getMethodInfo = methodInfos.FirstOrDefault(m => m.Name == getMethodBuilder.Name);
            if (getMethodInfo != null)
            {
                typeBuilder.DefineMethodOverride(getMethodBuilder, getMethodInfo);
            }

            MethodInfo setMethodInfo = methodInfos.FirstOrDefault(m => m.Name == setMethodBuilder.Name);
            if (setMethodInfo != null)
            {
                typeBuilder.DefineMethodOverride(setMethodBuilder, setMethodInfo);
            }
        }

        /// <summary>
        /// Create instance of <see cref="System.Reflection.Emit.MethodBuilder"/> for property setter.
        /// </summary>
        /// <param name="typeBuilder">Instance of <see cref="System.Reflection.Emit.TypeBuilder"/> for which to create the method builder.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="fieldBuilder">Instance of the <see cref="System.Reflection.Emit.FieldBuilder"/> for the underlying field of the property.</param>
        /// <param name="methodAttributes">Method attributes.</param>
        /// <returns>Instance of <see cref="MethodBuilder"/> for property setter.</returns>
        public static MethodBuilder CreateSetMethodBuilder(this TypeBuilder typeBuilder, string propertyName, Type propertyType, FieldBuilder fieldBuilder, MethodAttributes methodAttributes)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (fieldBuilder is null)
            {
                throw new ArgumentNullException(nameof(fieldBuilder));
            }

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + propertyName, methodAttributes, null, new[] { propertyType });

            ILGenerator ilgenerator = methodBuilder.GetILGenerator();

            ilgenerator.Emit(OpCodes.Ldarg_0);
            ilgenerator.Emit(OpCodes.Ldarg_1);
            ilgenerator.Emit(OpCodes.Stfld, fieldBuilder);
            ilgenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        /// <summary>
        /// Create instance of <see cref="System.Reflection.Emit.MethodBuilder"/> for property getter.
        /// </summary>
        /// <param name="typeBuilder">Instance of <see cref="System.Reflection.Emit.TypeBuilder"/> for which to create the method builder.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="fieldBuilder">Instance of the <see cref="System.Reflection.Emit.FieldBuilder"/> for the underlying field of the property.</param>
        /// <param name="methodAttributes">Method attributes.</param>
        /// <returns>Instance of <see cref="System.Reflection.Emit.MethodBuilder"/> for property setter.</returns>
        public static MethodBuilder CreateGetMethodBuilder(this TypeBuilder typeBuilder, string propertyName, Type propertyType, FieldBuilder fieldBuilder, MethodAttributes methodAttributes)
        {
            if (typeBuilder is null)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (fieldBuilder is null)
            {
                throw new ArgumentNullException(nameof(fieldBuilder));
            }

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + propertyName, methodAttributes, propertyType, Type.EmptyTypes);

            ILGenerator ilgenerator = methodBuilder.GetILGenerator();

            ilgenerator.Emit(OpCodes.Ldarg_0);
            ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            ilgenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }
    }
}
