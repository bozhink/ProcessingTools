// <copyright file="MapperConfigurationExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.AutoMapper
{
    using System;
    using global::AutoMapper;

    /// <summary>
    /// <see cref="MapperConfiguration"/> extensions.
    /// </summary>
    public static class MapperConfigurationExtensions
    {
        /// <summary>
        /// Creates a mapping configuration from the TSource type to the TDestination type using proxy type.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="configuration">Instance of <see cref="IMapperConfigurationExpression"/> to be updated.</param>
        public static void CreateMapUsingProxy<TSource, TDestination>(this IMapperConfigurationExpression configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            CreateMapUsingProxy(configuration, sourceType: typeof(TSource), destinationType: typeof(TDestination));
        }

        /// <summary>
        /// Creates a mapping configuration from the TSource type to the TDestination type using proxy type.
        /// Specify the member list to validate against during configuration validation.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="configuration">Instance of <see cref="IMapperConfigurationExpression"/> to be updated.</param>
        /// <param name="memberList">Member list to validate.</param>
        public static void CreateMapUsingProxy<TSource, TDestination>(this IMapperConfigurationExpression configuration, MemberList memberList)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            CreateMapUsingProxy(configuration, sourceType: typeof(TSource), destinationType: typeof(TDestination), memberList: memberList);
        }

        /// <summary>
        /// Creates a mapping configuration from the source type to the destination type using proxy type if destinationType is an interface.
        /// </summary>
        /// <param name="configuration">Instance of <see cref="IMapperConfigurationExpression"/> to be updated.</param>
        /// <param name="sourceType">Source type.</param>
        /// <param name="destinationType">Destination type.</param>
        public static void CreateMapUsingProxy(this IMapperConfigurationExpression configuration, Type sourceType, Type destinationType)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (sourceType is null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (destinationType.IsInterface)
            {
                Type destinationTypeProxy = DynamicProxyBuilder.ModuleBuilder.GetProxyTypeOf(destinationType);
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationTypeProxy);
                configuration.CreateMap(sourceType: destinationType, destinationType: destinationTypeProxy);
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationType).As(destinationTypeProxy);
            }
            else
            {
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationType);
            }
        }

        /// <summary>
        /// Creates a mapping configuration from the source type to the destination type using proxy type if destinationType is an interface.
        /// Specify the member list to validate against during configuration validation.
        /// </summary>
        /// <param name="configuration">Instance of <see cref="IMapperConfigurationExpression"/> to be updated.</param>
        /// <param name="sourceType">Source type.</param>
        /// <param name="destinationType">Destination type.</param>
        /// <param name="memberList">Member list to validate.</param>
        public static void CreateMapUsingProxy(this IMapperConfigurationExpression configuration, Type sourceType, Type destinationType, MemberList memberList)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (sourceType is null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (destinationType.IsInterface)
            {
                Type destinationTypeProxy = DynamicProxyBuilder.ModuleBuilder.GetProxyTypeOf(destinationType);
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationTypeProxy, memberList: memberList);
                configuration.CreateMap(sourceType: destinationType, destinationType: destinationTypeProxy, memberList: memberList);
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationType, memberList: memberList).As(destinationTypeProxy);
            }
            else
            {
                configuration.CreateMap(sourceType: sourceType, destinationType: destinationType, memberList: memberList);
            }
        }
    }
}
