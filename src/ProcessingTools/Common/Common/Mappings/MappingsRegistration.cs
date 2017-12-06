namespace ProcessingTools.Common.Mappings
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using AutoMapper.Configuration;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts.Models;

    public class MappingsRegistration
    {
        private readonly MapperConfigurationExpression mapperConfigurationExpression;
        private readonly Lazy<MapperConfiguration> mapperConfiguration;

        public MappingsRegistration(params string[] assemblies)
            : this(assemblies.GetAssemblies().ToArray())
        {
        }

        public MappingsRegistration(params Assembly[] assemblies)
            : this(assemblies.GetExportedTypes().ToArray())
        {
        }

        public MappingsRegistration(params Type[] types)
        {
            this.mapperConfigurationExpression = new MapperConfigurationExpression();

            this.LoadStandardMappings(types);
            this.LoadCustomMappings(types);

            this.mapperConfiguration = new Lazy<MapperConfiguration>(() => new MapperConfiguration(this.mapperConfigurationExpression), isThreadSafe: false);
        }

        public IMapper Mapper => this.mapperConfiguration.Value.CreateMapper();

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
