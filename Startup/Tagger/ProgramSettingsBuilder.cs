namespace ProcessingTools.MainProgram
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Configurator;
    using Contracts.Log;

    public class ProgramSettingsBuilder
    {
        private string[] args;

        private ProgramSettings settings;

        private ILogger logger;

        private List<int> arguments = new List<int>();
        private List<int> doubleDashedOptions = new List<int>();
        private List<int> singleDashedOptions = new List<int>();

        public ProgramSettingsBuilder(ILogger logger, string[] args)
        {
            this.logger = logger;
            this.args = args;
            this.Settings = new ProgramSettings();
            this.ParseConfigFiles();
            this.InitialCheckOfInputParameters(this.args);
            this.ParseFileNames(this.args);
            this.ParseSingleDashedOptions(this.args);
            this.ParseDoubleDashedOptions(this.args);
        }

        public ProgramSettings Settings
        {
            get
            {
                return this.settings;
            }

            private set
            {
                if (value != null)
                {
                    this.settings = value;
                }
                else
                {
                    throw new ArgumentNullException("ProgramSetting object should not be null.");
                }
            }
        }

        private void ParseConfigFiles()
        {
            try
            {
                var appConfigReader = new AppSettingsReader();
                string configJsonFilePath = appConfigReader.GetValue("ConfigJsonFilePath", typeof(string)).ToString();

                this.Settings.Config = ConfigBuilder.CreateConfig(configJsonFilePath);
            }
            catch
            {
                throw;
            }
        }

        private void InitialCheckOfInputParameters(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; ++i)
                {
                    char[] arg = args[i].ToCharArray();
                    if (arg[0] == '-' && arg.Length > 1 && arg[1] == '-')
                    {
                        this.doubleDashedOptions.Add(i);
                    }
                    else if (arg[0] == '-' || arg[0] == '/')
                    {
                        this.singleDashedOptions.Add(i);
                    }
                    else
                    {
                        this.arguments.Add(i);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void ParseFileNames(string[] args)
        {
            try
            {
                if (this.arguments.Count < 1)
                {
                    this.PrintHelp();
                }
                else if (this.arguments.Count == 1)
                {
                    this.Settings.InputFileName = args[this.arguments[0]];
                    this.Settings.OutputFileName = null;
                }
                else if (this.arguments.Count == 2)
                {
                    this.Settings.InputFileName = args[this.arguments[0]];
                    this.Settings.OutputFileName = args[this.arguments[1]];
                }
                else
                {
                    this.Settings.InputFileName = args[this.arguments[0]];
                    this.Settings.OutputFileName = args[this.arguments[1]];
                    this.Settings.QueryFileName = args[this.arguments[2]];
                }
            }
            catch
            {
                throw;
            }
        }

        private void ParseSingleDashedOptions(string[] args)
        {
            if (this.singleDashedOptions?.Count > 0)
            {
                foreach (int item in this.singleDashedOptions)
                {
                    char[] arg = args[item].ToCharArray();
                    if (arg.Length > 1)
                    {
                        for (int i = 1; i < arg.Length; i++)
                        {
                            switch (arg[i])
                            {
                                case 'h':
                                case '?':
                                    this.PrintHelp();
                                    break;

                                case 'i':
                                    this.Settings.InitialFormat = true;
                                    break;

                                case 't':
                                    this.Settings.FormatTreat = true;
                                    break;

                                case 'A':
                                    this.Settings.TagLowerTaxa = true;
                                    break;

                                case 'B':
                                    this.Settings.TagHigherTaxa = true;
                                    break;

                                case 'C':
                                    this.Settings.ParseLowerTaxa = true;
                                    break;

                                case 'D':
                                    this.Settings.ParseHigherTaxa = true;
                                    break;

                                case 'E':
                                    this.Settings.ExpandLowerTaxa = true;
                                    break;

                                case 'u':
                                    this.Settings.UntagSplit = true;
                                    break;

                                case 'w':
                                    this.Settings.TagWebLinks = true;
                                    break;

                                case 'd':
                                    this.Settings.TagDoi = true;
                                    break;

                                case 'f':
                                    this.Settings.TagFloats = true;
                                    break;

                                case 'm':
                                    this.Settings.ResolveMediaTypes = true;
                                    break;

                                case 'c':
                                    this.Settings.TagCoordinates = true;
                                    break;

                                case 'k':
                                    this.Settings.ParseCoordinates = true;
                                    break;

                                case '1':
                                    this.Settings.Flag1 = true;
                                    break;

                                case '2':
                                    this.Settings.Flag2 = true;
                                    break;

                                case '3':
                                    this.Settings.Flag3 = true;
                                    break;

                                case '4':
                                    this.Settings.Flag4 = true;
                                    break;

                                case '5':
                                    this.Settings.Flag5 = true;
                                    break;

                                case '6':
                                    this.Settings.Flag6 = true;
                                    break;

                                case '7':
                                    this.Settings.Flag7 = true;
                                    break;

                                case '8':
                                    this.Settings.Flag8 = true;
                                    break;

                                case 'X':
                                    this.Settings.QueryReplace = true;
                                    break;

                                case 'z':
                                    this.Settings.ZoobankCloneXml = true;
                                    break;

                                case 'r':
                                    this.Settings.ParseReferences = true;
                                    break;

                                case 'R':
                                    this.Settings.TagReferences = true;
                                    break;

                                case 'Q':
                                    this.Settings.QuentinSpecificActions = true;
                                    break;

                                case 'F':
                                    this.Settings.Flora = true;
                                    break;

                                case 's':
                                    this.Settings.ParseBySection = true;
                                    break;

                                case 'V':
                                    this.Settings.ValidateTaxa = true;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void ParseDoubleDashedOptions(string[] args)
        {
            if (this.doubleDashedOptions?.Count > 0)
            {
                foreach (int item in this.doubleDashedOptions)
                {
                    if (args[item].CompareTo("--split-aphia") == 0)
                    {
                        this.Settings.ParseHigherWithAphia = true;
                    }
                    else if (args[item].CompareTo("--split-col") == 0)
                    {
                        this.Settings.ParseHigherWithCoL = true;
                    }
                    else if (args[item].CompareTo("--split-gbif") == 0)
                    {
                        this.Settings.ParseHigherWithGbif = true;
                    }
                    else if (args[item].CompareTo("--split-suffix") == 0)
                    {
                        this.Settings.ParseHigherBySuffix = true;
                    }
                    else if (args[item].CompareTo("--above-genus") == 0)
                    {
                        this.Settings.ParseHigherAboveGenus = true;
                    }
                    else if (args[item].CompareTo("--system") == 0)
                    {
                        this.Settings.Config.ArticleSchemaType = SchemaType.System;
                    }
                    else if (args[item].CompareTo("--nlm") == 0)
                    {
                        this.Settings.Config.ArticleSchemaType = SchemaType.Nlm;
                    }
                    else if (args[item].CompareTo("--test") == 0)
                    {
                        this.Settings.TestFlag = true;
                    }
                    else if (args[item].CompareTo("--extract-taxa") == 0 || args[item].CompareTo("--et") == 0)
                    {
                        this.Settings.ExtractTaxa = true;
                    }
                    else if (args[item].CompareTo("--extract-lower-taxa") == 0 || args[item].CompareTo("--elt") == 0)
                    {
                        this.Settings.ExtractLowerTaxa = true;
                    }
                    else if (args[item].CompareTo("--extract-higher-taxa") == 0 || args[item].CompareTo("--eht") == 0)
                    {
                        this.Settings.ExtractHigherTaxa = true;
                    }
                    else if (args[item].CompareTo("--zoobank-nlm") == 0)
                    {
                        this.Settings.ZoobankGenerateRegistrationXml = true;
                    }
                    else if (args[item].CompareTo("--zoobank-json") == 0)
                    {
                        this.Settings.ZoobankCloneJson = true;
                    }
                    else if (args[item].CompareTo("--zoobank-clone") == 0)
                    {
                        this.Settings.ZoobankCloneXml = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-aphia") == 0 || args[item].CompareTo("--ptm-aphia") == 0)
                    {
                        this.Settings.ParseTreatmentMetaWithAphia = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-gbif") == 0 || args[item].CompareTo("--ptm-gbif") == 0)
                    {
                        this.Settings.ParseTreatmentMetaWithGbif = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-col") == 0 || args[item].CompareTo("--ptm-col") == 0)
                    {
                        this.Settings.ParseTreatmentMetaWithCol = true;
                    }
                    else if (args[item].CompareTo("--table-fn") == 0)
                    {
                        this.Settings.TagTableFn = true;
                    }
                    else if (args[item].CompareTo("--environments") == 0)
                    {
                        this.Settings.TagEnvironments = true;
                    }
                    else if (args[item].CompareTo("--codes") == 0)
                    {
                        this.Settings.TagCodes = true;
                    }
                    else if (args[item].CompareTo("--quantities") == 0)
                    {
                        this.Settings.TagQuantities = true;
                    }
                    else if (args[item].CompareTo("--dates") == 0)
                    {
                        this.Settings.TagDates = true;
                    }
                    else if (args[item].CompareTo("--abbrev") == 0)
                    {
                        this.Settings.TagAbbreviations = true;
                    }
                    else if (args[item].CompareTo("--envo") == 0)
                    {
                        this.Settings.TagEnvo = true;
                    }
                    else if (args[item].CompareTo("--validate-taxa") == 0)
                    {
                        this.Settings.ValidateTaxa = true;
                    }
                    else if (args[item].CompareTo("--xsl") == 0)
                    {
                        this.Settings.RunXslTransform = true;
                    }
                    else if (args[item].CompareTo("--products") == 0)
                    {
                        this.Settings.TagProducts = true;
                    }
                    else if (args[item].CompareTo("--institutions") == 0)
                    {
                        this.Settings.TagInstitutions = true;
                    }
                    else if (args[item].CompareTo("--morphology") == 0)
                    {
                        this.Settings.TagMorphologicalEpithets = true;
                    }
                }
            }
        }

        private void PrintHelp()
        {
            this.logger?.Log(Messages.HelpMessage);
            Environment.Exit(1);
        }
    }
}
