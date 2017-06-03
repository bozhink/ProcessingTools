namespace ProcessingTools.Web
{
    using System.Reflection;
    using System.Web.Mvc;
    using AutoMapper;
    using ProcessingTools.Common.Mappings;

    public static class AutoMapperConfig
    {
        private static IMapper mapper;

        public static IMapper Mapper => mapper;

        public static void RegisterMappings(params string[] assemblies)
        {
            var registration = new InnerMappingsRegistration(assemblies);
            mapper = registration.Mapper;
        }

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            var registration = new InnerMappingsRegistration(assemblies);
            mapper = registration.Mapper;
        }

        private class InnerMappingsRegistration : MappingsRegistration
        {
            public InnerMappingsRegistration(params string[] assemblies)
                : base(assemblies)
            {
                this.MapperConfigurationExpression.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));
            }

            public InnerMappingsRegistration(params Assembly[] assemblies)
                : base(assemblies)
            {
                this.MapperConfigurationExpression.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));
            }
        }
    }
}
