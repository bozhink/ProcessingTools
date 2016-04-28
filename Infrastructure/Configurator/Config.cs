namespace ProcessingTools.Configurator
{
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Config
    {
        [DataMember(Name = "blackListXmlFilePath")]
        public string BlackListXmlFilePath { get; set; }

        [DataMember(Name = "rankListXmlFilePath")]
        public string RankListXmlFilePath { get; set; }
    }
}