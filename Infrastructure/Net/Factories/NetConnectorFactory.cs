namespace ProcessingTools.Net.Factories
{
    using Contracts;
    using ProcessingTools.Net;
    using ProcessingTools.Net.Contracts;

    public class NetConnectorFactory : INetConnectorFactory
    {
        public INetConnector Create() => new NetConnector();

        public INetConnector Create(string baseAddress) => new NetConnector(baseAddress);
    }
}
