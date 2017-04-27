namespace ProcessingTools.Net.Factories.Contracts
{
    using ProcessingTools.Net.Contracts;

    public interface INetConnectorFactory
    {
        INetConnector Create();

        INetConnector Create(string baseAddress);
    }
}
