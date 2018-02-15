namespace ProcessingTools.Processors.Contracts.Bio.ZooBank
{
    using ProcessingTools.Contracts.Xml;

    public interface IRegistrationTransformersFactory
    {
        IXmlTransformer GetZooBankRegistrationTransformer();
    }
}
