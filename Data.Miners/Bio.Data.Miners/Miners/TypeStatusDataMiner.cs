namespace ProcessingTools.Bio.Data.Miners.Miners
{
    using Contracts;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;

    public class TypeStatusDataMiner : SimpleServiceStringDataMinerFactory<ITypeStatusDataService, ITypeStatus>, ITypeStatusDataMiner
    {
        public TypeStatusDataMiner(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
