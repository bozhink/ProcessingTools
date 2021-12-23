// <copyright file="DynamicProxyGenerator.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;

    /// <summary>
    /// Dynamic proxy generator.
    /// See http://geekswithblogs.net/abhijeetp/archive/2010/04/04/a-simple-dynamic-proxy.aspx.
    /// </summary>
    public static class DynamicProxyGenerator
    {
        /// <summary>
        /// Get fake instance for specified interface type.
        /// Fake instance means that methods and properties does not set or get data, but
        /// the instance implements the specified interface type.
        /// </summary>
        /// <typeparam name="T">Interface type to be instantiated.</typeparam>
        /// <returns>Fake instance of type T.</returns>
        /// <exception cref="InvalidOperationException">If the type T is not interface.</exception>
        public static T GetFakeInstanceFor<T>()
        {
            Type typeOfT = typeof(T);
            if (!typeOfT.IsInterface)
            {
                throw new InvalidOperationException();
            }

            Type constructedType = DynamicProxyBuilder.ModuleBuilder.GetProxyTypeOf(typeOfT);
            return (T)Activator.CreateInstance(constructedType);
        }
    }
}
