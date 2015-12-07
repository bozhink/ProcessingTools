namespace ProcessingTools.Infrastructure.Net
{
    using System.Text;

    public partial class Connector
    {
        public const string DefaultContentType = "text/plain; encoding='utf-8'";
        public const string JsonMediaType = "application/json";
        public const string XmlMediaType = "application/xml";
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
    }
}