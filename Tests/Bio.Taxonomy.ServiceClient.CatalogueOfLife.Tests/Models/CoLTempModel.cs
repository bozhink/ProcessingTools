namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests.Models
{
    using System.Xml.Serialization;

    public class CoLTempModel
    {
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ResponseModel
    {
        private Result[] resultField;

        private string idField;

        private string nameField;

        private byte total_number_of_resultsField;

        private byte number_of_results_returnedField;

        private byte startField;

        private string error_messageField;

        private string versionField;

        private string rankField;

        /// <remarks/>
        [XmlElement("result")]
        public Result[] result
        {
            get
            {
                return this.resultField;
            }

            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }

            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public byte total_number_of_results
        {
            get
            {
                return this.total_number_of_resultsField;
            }

            set
            {
                this.total_number_of_resultsField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public byte number_of_results_returned
        {
            get
            {
                return this.number_of_results_returnedField;
            }

            set
            {
                this.number_of_results_returnedField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public byte start
        {
            get
            {
                return this.startField;
            }

            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string error_message
        {
            get
            {
                return this.error_messageField;
            }

            set
            {
                this.error_messageField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }

            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class Result
    {
        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [XmlElement("accepted_name", typeof(resultsResultAccepted_name))]
        [XmlElement("author", typeof(string))]
        [XmlElement("bibliographic_citation", typeof(string))]
        [XmlElement("child_taxa", typeof(resultsResultChild_taxa))]
        [XmlElement("classification", typeof(resultsResultClassification))]
        [XmlElement("common_names", typeof(object))]
        [XmlElement("distribution", typeof(string))]
        [XmlElement("genus", typeof(string))]
        [XmlElement("id", typeof(string))]
        [XmlElement("infraspecies", typeof(object))]
        [XmlElement("infraspecies_marker", typeof(object))]
        [XmlElement("is_extinct", typeof(bool))]
        [XmlElement("name", typeof(string))]
        [XmlElement("name_html", typeof(resultsResultName_html))]
        [XmlElement("name_status", typeof(string))]
        [XmlElement("online_resource", typeof(string))]
        [XmlElement("rank", typeof(string))]
        [XmlElement("record_scrutiny_date", typeof(resultsResultRecord_scrutiny_date))]
        [XmlElement("references", typeof(resultsResultReferences))]
        [XmlElement("source_database", typeof(string))]
        [XmlElement("source_database_url", typeof(string))]
        [XmlElement("species", typeof(string))]
        [XmlElement("subgenus", typeof(object))]
        [XmlElement("synonyms", typeof(object))]
        [XmlElement("url", typeof(string))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }

            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }

            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class resultsResultAccepted_name
    {
        private string idField;

        private string nameField;

        private string rankField;

        private string name_statusField;

        private string genusField;

        private object subgenusField;

        private string speciesField;

        private object infraspecies_markerField;

        private object infraspeciesField;

        private string authorField;

        private resultsResultAccepted_nameRecord_scrutiny_date record_scrutiny_dateField;

        private string online_resourceField;

        private bool is_extinctField;

        private string source_databaseField;

        private string source_database_urlField;

        private string bibliographic_citationField;

        private resultsResultAccepted_nameName_html name_htmlField;

        private string urlField;

        private object distributionField;

        private resultsResultAccepted_nameReference[] referencesField;

        private resultsResultAccepted_nameTaxon[] classificationField;

        private object child_taxaField;

        private resultsResultAccepted_nameSynonym[] synonymsField;

        private resultsResultAccepted_nameCommon_names common_namesField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }

            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }

            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string rank
        {
            get
            {
                return this.rankField;
            }

            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        public string name_status
        {
            get
            {
                return this.name_statusField;
            }

            set
            {
                this.name_statusField = value;
            }
        }

        /// <remarks/>
        public string genus
        {
            get
            {
                return this.genusField;
            }

            set
            {
                this.genusField = value;
            }
        }

        /// <remarks/>
        public object subgenus
        {
            get
            {
                return this.subgenusField;
            }

            set
            {
                this.subgenusField = value;
            }
        }

        /// <remarks/>
        public string species
        {
            get
            {
                return this.speciesField;
            }

            set
            {
                this.speciesField = value;
            }
        }

        /// <remarks/>
        public object infraspecies_marker
        {
            get
            {
                return this.infraspecies_markerField;
            }

            set
            {
                this.infraspecies_markerField = value;
            }
        }

        /// <remarks/>
        public object infraspecies
        {
            get
            {
                return this.infraspeciesField;
            }

            set
            {
                this.infraspeciesField = value;
            }
        }

        /// <remarks/>
        public string author
        {
            get
            {
                return this.authorField;
            }

            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public resultsResultAccepted_nameRecord_scrutiny_date record_scrutiny_date
        {
            get
            {
                return this.record_scrutiny_dateField;
            }

            set
            {
                this.record_scrutiny_dateField = value;
            }
        }

        /// <remarks/>
        public string online_resource
        {
            get
            {
                return this.online_resourceField;
            }

            set
            {
                this.online_resourceField = value;
            }
        }

        /// <remarks/>
        public bool is_extinct
        {
            get
            {
                return this.is_extinctField;
            }

            set
            {
                this.is_extinctField = value;
            }
        }

        /// <remarks/>
        public string source_database
        {
            get
            {
                return this.source_databaseField;
            }

            set
            {
                this.source_databaseField = value;
            }
        }

        /// <remarks/>
        public string source_database_url
        {
            get
            {
                return this.source_database_urlField;
            }

            set
            {
                this.source_database_urlField = value;
            }
        }

        /// <remarks/>
        public string bibliographic_citation
        {
            get
            {
                return this.bibliographic_citationField;
            }

            set
            {
                this.bibliographic_citationField = value;
            }
        }

        /// <remarks/>
        public resultsResultAccepted_nameName_html name_html
        {
            get
            {
                return this.name_htmlField;
            }

            set
            {
                this.name_htmlField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }

            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public object distribution
        {
            get
            {
                return this.distributionField;
            }

            set
            {
                this.distributionField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("reference", IsNullable = false)]
        public resultsResultAccepted_nameReference[] references
        {
            get
            {
                return this.referencesField;
            }

            set
            {
                this.referencesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("taxon", IsNullable = false)]
        public resultsResultAccepted_nameTaxon[] classification
        {
            get
            {
                return this.classificationField;
            }

            set
            {
                this.classificationField = value;
            }
        }

        /// <remarks/>
        public object child_taxa
        {
            get
            {
                return this.child_taxaField;
            }
            set
            {
                this.child_taxaField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("synonym", IsNullable = false)]
        public resultsResultAccepted_nameSynonym[] synonyms
        {
            get
            {
                return this.synonymsField;
            }

            set
            {
                this.synonymsField = value;
            }
        }

        /// <remarks/>
        public resultsResultAccepted_nameCommon_names common_names
        {
            get
            {
                return this.common_namesField;
            }

            set
            {
                this.common_namesField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class resultsResultAccepted_nameRecord_scrutiny_date
    {
        private string scrutinyField;

        /// <remarks/>
        public string scrutiny
        {
            get
            {
                return this.scrutinyField;
            }

            set
            {
                this.scrutinyField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class resultsResultAccepted_nameName_html
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }

            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }

            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class resultsResultAccepted_nameReference
    {
        private string authorField;

        private ushort yearField;

        private string titleField;

        private string sourceField;

        /// <remarks/>
        public string author
        {
            get
            {
                return this.authorField;
            }

            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public ushort year
        {
            get
            {
                return this.yearField;
            }

            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }

            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string source
        {
            get
            {
                return this.sourceField;
            }

            set
            {
                this.sourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultAccepted_nameTaxon
    {
        private string idField;

        private string nameField;

        private string rankField;

        private resultsResultAccepted_nameTaxonName_html name_htmlField;

        private string urlField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }

            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }

            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string rank
        {
            get
            {
                return this.rankField;
            }

            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        public resultsResultAccepted_nameTaxonName_html name_html
        {
            get
            {
                return this.name_htmlField;
            }

            set
            {
                this.name_htmlField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }

            set
            {
                this.urlField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class resultsResultAccepted_nameTaxonName_html
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultAccepted_nameSynonym
    {
        private string idField;

        private string nameField;

        private string rankField;

        private string name_statusField;

        private string genusField;

        private object subgenusField;

        private string speciesField;

        private object infraspecies_markerField;

        private object infraspeciesField;

        private string authorField;

        private object record_scrutiny_dateField;

        private object online_resourceField;

        private byte is_extinctField;

        private resultsResultAccepted_nameSynonymName_html name_htmlField;

        private string urlField;

        private object referencesField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        public string name_status
        {
            get
            {
                return this.name_statusField;
            }
            set
            {
                this.name_statusField = value;
            }
        }

        /// <remarks/>
        public string genus
        {
            get
            {
                return this.genusField;
            }
            set
            {
                this.genusField = value;
            }
        }

        /// <remarks/>
        public object subgenus
        {
            get
            {
                return this.subgenusField;
            }
            set
            {
                this.subgenusField = value;
            }
        }

        /// <remarks/>
        public string species
        {
            get
            {
                return this.speciesField;
            }
            set
            {
                this.speciesField = value;
            }
        }

        /// <remarks/>
        public object infraspecies_marker
        {
            get
            {
                return this.infraspecies_markerField;
            }
            set
            {
                this.infraspecies_markerField = value;
            }
        }

        /// <remarks/>
        public object infraspecies
        {
            get
            {
                return this.infraspeciesField;
            }
            set
            {
                this.infraspeciesField = value;
            }
        }

        /// <remarks/>
        public string author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public object record_scrutiny_date
        {
            get
            {
                return this.record_scrutiny_dateField;
            }
            set
            {
                this.record_scrutiny_dateField = value;
            }
        }

        /// <remarks/>
        public object online_resource
        {
            get
            {
                return this.online_resourceField;
            }
            set
            {
                this.online_resourceField = value;
            }
        }

        /// <remarks/>
        public byte is_extinct
        {
            get
            {
                return this.is_extinctField;
            }
            set
            {
                this.is_extinctField = value;
            }
        }

        /// <remarks/>
        public resultsResultAccepted_nameSynonymName_html name_html
        {
            get
            {
                return this.name_htmlField;
            }
            set
            {
                this.name_htmlField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public object references
        {
            get
            {
                return this.referencesField;
            }
            set
            {
                this.referencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultAccepted_nameSynonymName_html
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultAccepted_nameCommon_names
    {
        private resultsResultAccepted_nameCommon_namesCommon_name common_nameField;

        /// <remarks/>
        public resultsResultAccepted_nameCommon_namesCommon_name common_name
        {
            get
            {
                return this.common_nameField;
            }
            set
            {
                this.common_nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultAccepted_nameCommon_namesCommon_name
    {
        private string nameField;

        private string languageField;

        private string countryField;

        private object referencesField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public object references
        {
            get
            {
                return this.referencesField;
            }
            set
            {
                this.referencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultChild_taxa
    {
        private resultsResultChild_taxaTaxon[] taxonField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("taxon")]
        public resultsResultChild_taxaTaxon[] taxon
        {
            get
            {
                return this.taxonField;
            }
            set
            {
                this.taxonField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultChild_taxaTaxon
    {
        private string idField;

        private string nameField;

        private string rankField;

        private string name_htmlField;

        private string urlField;

        private bool is_extinctField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        public string name_html
        {
            get
            {
                return this.name_htmlField;
            }
            set
            {
                this.name_htmlField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public bool is_extinct
        {
            get
            {
                return this.is_extinctField;
            }
            set
            {
                this.is_extinctField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultClassification
    {
        private resultsResultClassificationTaxon[] taxonField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("taxon")]
        public resultsResultClassificationTaxon[] taxon
        {
            get
            {
                return this.taxonField;
            }
            set
            {
                this.taxonField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultClassificationTaxon
    {
        private string idField;

        private string nameField;

        private string rankField;

        private resultsResultClassificationTaxonName_html name_htmlField;

        private string urlField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        public resultsResultClassificationTaxonName_html name_html
        {
            get
            {
                return this.name_htmlField;
            }
            set
            {
                this.name_htmlField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultClassificationTaxonName_html
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultName_html
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultRecord_scrutiny_date
    {
        private string scrutinyField;

        /// <remarks/>
        public string scrutiny
        {
            get
            {
                return this.scrutinyField;
            }
            set
            {
                this.scrutinyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultReferences
    {
        private resultsResultReferencesReference referenceField;

        /// <remarks/>
        public resultsResultReferencesReference reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultReferencesReference
    {
        private string authorField;

        private ushort yearField;

        private resultsResultReferencesReferenceTitle titleField;

        private resultsResultReferencesReferenceSource sourceField;

        /// <remarks/>
        public string author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        public ushort year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        public resultsResultReferencesReferenceTitle title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public resultsResultReferencesReferenceSource source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultReferencesReferenceTitle
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class resultsResultReferencesReferenceSource
    {
        private string iField;

        private string[] textField;

        /// <remarks/>
        public string i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks/>
        accepted_name,

        /// <remarks/>
        author,

        /// <remarks/>
        bibliographic_citation,

        /// <remarks/>
        child_taxa,

        /// <remarks/>
        classification,

        /// <remarks/>
        common_names,

        /// <remarks/>
        distribution,

        /// <remarks/>
        genus,

        /// <remarks/>
        id,

        /// <remarks/>
        infraspecies,

        /// <remarks/>
        infraspecies_marker,

        /// <remarks/>
        is_extinct,

        /// <remarks/>
        name,

        /// <remarks/>
        name_html,

        /// <remarks/>
        name_status,

        /// <remarks/>
        online_resource,

        /// <remarks/>
        rank,

        /// <remarks/>
        record_scrutiny_date,

        /// <remarks/>
        references,

        /// <remarks/>
        source_database,

        /// <remarks/>
        source_database_url,

        /// <remarks/>
        species,

        /// <remarks/>
        subgenus,

        /// <remarks/>
        synonyms,

        /// <remarks/>
        url,
    }
}