namespace ProcessingTools.Configurator
{
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Config
    {
        [DataMember(Name = "blackListCleanXslPath")]
        public string BlackListCleanXslPath { get; set; }

        [DataMember(Name = "blackListXmlFilePath")]
        public string BlackListXmlFilePath { get; set; }

        [DataMember(Name = "rankListCleanXslPath")]
        public string RankListCleanXslPath { get; set; }

        [DataMember(Name = "rankListXmlFilePath")]
        public string RankListXmlFilePath { get; set; }

        [DataMember(Name = "whiteListCleanXslPath")]
        public string WhiteListCleanXslPath { get; set; }

        [DataMember(Name = "whiteListXmlFilePath")]
        public string WhiteListXmlFilePath { get; set; }
    }
}