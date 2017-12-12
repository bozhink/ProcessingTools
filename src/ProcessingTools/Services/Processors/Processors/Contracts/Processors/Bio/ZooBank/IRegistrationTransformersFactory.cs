namespace ProcessingTools.Contracts.Processors.Factories
{
    using ProcessingTools.Contracts.Xml;

    public interface IRegistrationTransformersFactory
    {
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
