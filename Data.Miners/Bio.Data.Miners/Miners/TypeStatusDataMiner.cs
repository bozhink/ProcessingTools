namespace ProcessingTools.Bio.Data.Miners.Miners
{
    using Contracts;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Data.Miners.Common.Factories;

    public class TypeStatusDataMiner : SimpleServiceStringDataMinerFactory<ITypeStatusDataService, TypeStatusServiceModel>, ITypeStatusDataMiner
    {
        public TypeStatusDataMiner(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
