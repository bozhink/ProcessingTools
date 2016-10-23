namespace ProcessingTools.Xml.Contracts.Providers
{
    using Contracts;
    using ProcessingTools.Contracts;

    public interface IXQueryTransformProvider : IProvider
    {
        IXQueryTransform GetXQueryTransform();
    }
}
