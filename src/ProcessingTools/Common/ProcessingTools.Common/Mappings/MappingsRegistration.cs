// <copyright file="MappingsRegistration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Mappings
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using AutoMapper.Configuration;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Mappings Registration.
    /// </summary>
    public class MappingsRegistration
    {
        private readonly MapperConfigurationExpression mapperConfigurationExpression;
        private readonly Lazy<MapperConfiguration> mapperConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingsRegistration"/> class.
        /// </summary>
        /// <param name="assemblies">Names of assemblies to be loaded.</param>
        public MappingsRegistration(params string[] assemblies)
            : this(assemblies.GetAssemblies().ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingsRegistration"/> class.
        /// </summary>
        /// <param name="assemblies">Assemblies to be loaded.</param>
        public MappingsRegistration(params Assembly[] assemblies)
            : this(assemblies.GetExportedTypes().ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingsRegistration"/> class.
        /// </summary>
        /// <param name="types">Types to be loaded.</param>
        public MappingsRegistration(params Type[] types)
        {
            this.mapperConfigurationExpression = new MapperConfigurationExpression();

            this.LoadStandardMappings(types);
            this.LoadCustomMappings(types);

            this.mapperConfiguration = new Lazy<MapperConfiguration>(() => new MapperConfiguration(this.mapperConfigurationExpression), isThreadSafe: false);
        }

        /// <summary>
        /// Gets the <see cref="IMapper"/> instance.
        /// </summary>
        public IMapper Mapper => this.mapperConfiguration.Value.CreateMapper();

        /// <summary>
        /// Gets the <see cref="IMapperConfigurationExpression"/> instance.
        /// </summary>
        protected IMapperConfigurationExpression MapperConfigurationExpression => this.mapperConfigurationExpression;

        private void LoadStandardMappings(params Type[] types)
        {
            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType &&
                        type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.t.IsAbstract &&
                        !type.t.IsInterface)
                .Select(type => new
                {
                    Source = type.i.GetGenericArguments()[0],
                    Destination = type.t
                });

            foreach (var map in maps)
            {
                this.MapperConfigurationExpression.CreateMap(map.Source, map.Destination);
                this.MapperConfigurationExpression.CreateMap(map.Destination, map.Source);
            }
        }

        private void LoadCustomMappings(params Type[] types)
        {
            var maps =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(IHaveCustomMappings).IsAssignableFrom(type.t) &&
                            !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(this.MapperConfigurationExpression);
            }
        }
    }
}
