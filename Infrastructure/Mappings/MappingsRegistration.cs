namespace ProcessingTools.Mappings
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using Contracts;
    using Extensions;

    public class MappingsRegistration
    {
        private readonly IMapperConfiguration mapperConfiguration;

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
            this.mapperConfiguration = new MapperConfiguration(c => { });

            this.LoadStandardMappings(types);
            this.LoadCustomMappings(types);
        }

        public IMapper Mapper => ((MapperConfiguration)this.mapperConfiguration).CreateMapper();

        protected IMapperConfiguration MapperConfiguration => this.mapperConfiguration;

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
                this.mapperConfiguration.CreateMap(map.Source, map.Destination);
                this.mapperConfiguration.CreateMap(map.Destination, map.Source);
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
                map.CreateMappings(this.mapperConfiguration);
            }
        }
    }
}