namespace ProcessingTools.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IRegistrationTransformersFactory
    {
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
