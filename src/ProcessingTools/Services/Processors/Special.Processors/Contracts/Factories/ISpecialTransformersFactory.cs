namespace ProcessingTools.Special.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface ISpecialTransformersFactory
    {
        IXmlTransformer GetGavinLaurensTransformer();
    }
}
