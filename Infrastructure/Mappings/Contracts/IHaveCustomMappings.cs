namespace ProcessingTools.Mappings.Contracts
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}