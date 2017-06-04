namespace ProcessingTools.Contracts.Net
{
    public interface INetConnectorFactory
    {
        INetConnector Create();

        INetConnector Create(string baseAddress);
    }
}
