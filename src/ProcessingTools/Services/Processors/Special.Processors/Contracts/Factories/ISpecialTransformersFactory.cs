namespace ProcessingTools.Special.Processors.Contracts.Factories
{
    using ProcessingTools.Processors.Contracts;

    public interface ISpecialTransformersFactory
    {
        IXmlTransformer GetGavinLaurensTransformer();
    }
}
