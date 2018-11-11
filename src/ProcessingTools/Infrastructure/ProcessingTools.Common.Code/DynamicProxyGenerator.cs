// <copyright file="DynamicProxyGenerator.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Dynamic proxy generator.
    /// See http://geekswithblogs.net/abhijeetp/archive/2010/04/04/a-simple-dynamic-proxy.aspx
    /// </summary>
    public static class DynamicProxyGenerator
    {
        /// <summary>
        /// Get instance for specified type.
        /// </summary>
        /// <typeparam name="T">Type to be instantiated.</typeparam>
        /// <returns>Instance of type T.</returns>
        public static T GetInstanceFor<T>()
        {
            Type typeOfT = typeof(T);
            var methodInfos = typeOfT.GetMethods();
            var assemblyName = new AssemblyName("testAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("testModule");
            var typeBuilder = moduleBuilder.DefineType(typeOfT.Name + "Proxy", TypeAttributes.Public);

            typeBuilder.AddInterfaceImplementation(typeOfT);
            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Array.Empty<Type>());
            var iLGenerator = constructorBuilder.GetILGenerator();
            iLGenerator.EmitWriteLine("Creating Proxy instance");
            iLGenerator.Emit(OpCodes.Ret);

            foreach (var methodInfo in methodInfos)
            {
                var parameterTypes = methodInfo.GetParameters().Select(p => p.GetType()).ToArray();
                var returnType = methodInfo.ReturnType;
                var attributes = MethodAttributes.Public | MethodAttributes.Virtual;

                var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, attributes: attributes, returnType: returnType, parameterTypes: parameterTypes);

                var methodILGen = methodBuilder.GetILGenerator();
                if (methodInfo.ReturnType == typeof(void))
                {
                    methodILGen.Emit(OpCodes.Ret);
                }
                else
                {
                    if (methodInfo.ReturnType.IsValueType || methodInfo.ReturnType.IsEnum)
                    {
                        Type typeOfType = typeof(Type);
                        MethodInfo getMethod = typeof(Activator).GetMethod(nameof(Activator.CreateInstance), new[] { typeOfType });
                        LocalBuilder localBuilder = methodILGen.DeclareLocal(methodInfo.ReturnType);
                        methodILGen.Emit(OpCodes.Ldtoken, localBuilder.LocalType);
                        methodILGen.Emit(OpCodes.Call, typeOfType.GetMethod(nameof(Type.GetTypeFromHandle)));
                        methodILGen.Emit(OpCodes.Callvirt, getMethod);
                        methodILGen.Emit(OpCodes.Unbox_Any, localBuilder.LocalType);
                    }
                    else
                    {
                        methodILGen.Emit(OpCodes.Ldnull);
                    }

                    methodILGen.Emit(OpCodes.Ret);
                }

                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }

            Type constructedType = typeBuilder.CreateTypeInfo();
            var instance = Activator.CreateInstance(constructedType);
            return (T)instance;
        }
    }
}
