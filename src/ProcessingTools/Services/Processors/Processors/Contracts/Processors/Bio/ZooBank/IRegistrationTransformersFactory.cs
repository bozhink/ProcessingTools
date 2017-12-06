namespace ProcessingTools.Contracts.Processors.Factories
{
    using ProcessingTools.Contracts;

    public interface IRegistrationTransformersFactory
    {
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
