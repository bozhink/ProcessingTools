namespace ProcessingTools.Net
{
    using ProcessingTools.Contracts;

    public class NetConnectorFactory : INetConnectorFactory
    {
        public INetConnector Create() => new NetConnector();

        public INetConnector Create(string baseAddress) => new NetConnector(baseAddress);
    }
}
