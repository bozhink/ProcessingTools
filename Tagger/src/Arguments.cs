namespace ProcessingTools.MainProgram
{
    using System.Collections.Generic;

    public partial class MainProcessingTool
    {
        private const int NumberOfExpandingIterations = 1;

        private static List<int> arguments = new List<int>();
        private static List<int> doubleDashedOptions = new List<int>();
        private static List<int> singleDashedOptions = new List<int>();

        private static ProgramSettings settings = new ProgramSettings();

        //private static Config config;
        
        //private static bool extractHigherTaxa = false;
        //private static bool extractLowerTaxa = false;
        //private static bool extractTaxa = false;
        //private static bool flag1 = false;
        //private static bool flag2 = false;
        //private static bool flag3 = false;
        //private static bool flag4 = false;
        //private static bool flag5 = false;
        //private static bool flag6 = false;
        //private static bool flag7 = false;
        //private static bool flag8 = false;
        //private static bool flora = false;
        //private static bool formatInit = false;
        //private static bool formatTreat = false;
        //private static string higherStructrureXpath = "//article";
        //private static string inputFileName = string.Empty;
        //private static string outputFileName = string.Empty;
        //private static bool parseBySection = false;
        //private static bool parseCoords = false;
        //private static bool parseReferences = false;
        //private static bool parseTreatmentMetaWithAphia = false;
        //private static bool parseTreatmentMetaWithCol = false;
        //private static bool parseTreatmentMetaWithGbif = false;
        //private static bool quentinSpecificActions = false;
        //private static string queryFileName = string.Empty;
        //private static bool queryReplace = false;
        //private static bool splitHigherAboveGenus = false;
        //private static bool splitHigherBySuffix = false;
        //private static bool splitHigherWithAphia = false;
        //private static bool splitHigherWithCoL = false;
        //private static bool splitHigherWithGbif = false;
        //private static bool tagAbbrev = false;
        //private static bool tagCodes = false;
        //private static bool tagCoords = false;
        //private static bool tagDates = false;
        //private static bool tagDoi = false;
        //private static bool tagEnvironments = false;
        //private static bool tagEnvo = false;
        //private static bool tagFigTab = false;
        //private static bool tagQuantities = false;
        //private static bool tagReferences = false;
        //private static bool tagTableFn = false;
        //private static bool tagWWW = false;
        //private static bool taxaA = false;
        //private static bool taxaB = false;
        //private static bool taxaC = false;
        //private static bool taxaD = false;
        //private static bool taxaE = false;
        //private static bool testFlag = false;
        //private static bool untagSplit = false;
        //private static bool validateTaxa = false;
        //private static bool zoobankCloneJson = false;
        //private static bool zoobankCloneXml = false;
        //private static bool zoobankGenerateRegistrationXml = false;

        private static ILogger consoleLogger = new ConsoleLogger();

        private static void ParseDoubleDashedOptions(string[] args)
        {
            if (doubleDashedOptions?.Count > 0)
            {
                foreach (int item in doubleDashedOptions)
                {
                    if (args[item].CompareTo("--split-aphia") == 0)
                    {
                        settings.SplitHigherWithAphia = true;
                    }
                    else if (args[item].CompareTo("--split-col") == 0)
                    {
                        settings.SplitHigherWithCoL = true;
                    }
                    else if (args[item].CompareTo("--split-gbif") == 0)
                    {
                        settings.SplitHigherWithGbif = true;
                    }
                    else if (args[item].CompareTo("--split-suffix") == 0)
                    {
                        settings.SplitHigherBySuffix = true;
                    }
                    else if (args[item].CompareTo("--above-genus") == 0)
                    {
                        settings.SplitHigherAboveGenus = true;
                    }
                    else if (args[item].CompareTo("--system") == 0)
                    {
                        settings.Config.NlmStyle = false;
                    }
                    else if (args[item].CompareTo("--nlm") == 0)
                    {
                        settings.Config.NlmStyle = true;
                    }
                    else if (args[item].CompareTo("--test") == 0)
                    {
                        settings.TestFlag = true;
                    }
                    else if (args[item].CompareTo("--extract-taxa") == 0 || args[item].CompareTo("--et") == 0)
                    {
                        settings.ExtractTaxa = true;
                    }
                    else if (args[item].CompareTo("--extract-lower-taxa") == 0 || args[item].CompareTo("--elt") == 0)
                    {
                        settings.ExtractLowerTaxa = true;
                    }
                    else if (args[item].CompareTo("--extract-higher-taxa") == 0 || args[item].CompareTo("--eht") == 0)
                    {
                        settings.ExtractHigherTaxa = true;
                    }
                    else if (args[item].CompareTo("--zoobank-nlm") == 0)
                    {
                        settings.ZoobankGenerateRegistrationXml = true;
                    }
                    else if (args[item].CompareTo("--zoobank-json") == 0)
                    {
                        settings.ZoobankCloneJson = true;
                    }
                    else if (args[item].CompareTo("--zoobank-clone") == 0)
                    {
                        settings.ZoobankCloneXml = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-aphia") == 0 || args[item].CompareTo("--ptm-aphia") == 0)
                    {
                        settings.ParseTreatmentMetaWithAphia = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-gbif") == 0 || args[item].CompareTo("--ptm-gbif") == 0)
                    {
                        settings.ParseTreatmentMetaWithGbif = true;
                    }
                    else if (args[item].CompareTo("--parse-treatment-meta-with-col") == 0 || args[item].CompareTo("--ptm-col") == 0)
                    {
                        settings.ParseTreatmentMetaWithCol = true;
                    }
                    else if (args[item].CompareTo("--table-fn") == 0)
                    {
                        settings.TagTableFn = true;
                    }
                    else if (args[item].CompareTo("--environments") == 0)
                    {
                        settings.TagEnvironments = true;
                    }
                    else if (args[item].CompareTo("--codes") == 0)
                    {
                        settings.TagCodes = true;
                    }
                    else if (args[item].CompareTo("--quantities") == 0)
                    {
                        settings.TagQuantities = true;
                    }
                    else if (args[item].CompareTo("--dates") == 0)
                    {
                        settings.TagDates = true;
                    }
                    else if (args[item].CompareTo("--abbrev") == 0)
                    {
                        settings.TagAbbrev = true;
                    }
                    else if (args[item].CompareTo("--envo") == 0)
                    {
                        settings.TagEnvo = true;
                    }
                    else if (args[item].CompareTo("--validate-taxa") == 0)
                    {
                        settings.ValidateTaxa = true;
                    }
                }
            }
        }

        private static void ParseSingleDashedOptions(string[] args)
        {
            if (singleDashedOptions?.Count > 0)
            {
                foreach (int item in singleDashedOptions)
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
                                    Alert.PrintHelp();
                                    Alert.Exit(0);
                                    break;

                                case 'i':
                                    settings.FormatInit = true;
                                    break;

                                case 't':
                                    settings.FormatTreat = true;
                                    break;

                                case 'A':
                                    settings.TaxaA = true;
                                    break;

                                case 'B':
                                    settings.TaxaB = true;
                                    break;

                                case 'C':
                                    settings.TaxaC = true;
                                    break;

                                case 'D':
                                    settings.TaxaD = true;
                                    break;

                                case 'E':
                                    settings.TaxaE = true;
                                    break;

                                case 'u':
                                    settings.UntagSplit = true;
                                    break;

                                case 'w':
                                    settings.TagWWW = true;
                                    break;

                                case 'd':
                                    settings.TagDoi = true;
                                    break;

                                case 'f':
                                    settings.TagFigTab = true;
                                    break;

                                case 'c':
                                    settings.TagCoords = true;
                                    break;

                                case 'k':
                                    settings.ParseCoords = true;
                                    break;

                                case '1':
                                    settings.Flag1 = true;
                                    break;

                                case '2':
                                    settings.Flag2 = true;
                                    break;

                                case '3':
                                    settings.Flag3 = true;
                                    break;

                                case '4':
                                    settings.Flag4 = true;
                                    break;

                                case '5':
                                    settings.Flag5 = true;
                                    break;

                                case '6':
                                    settings.Flag6 = true;
                                    break;

                                case '7':
                                    settings.Flag7 = true;
                                    break;

                                case '8':
                                    settings.Flag8 = true;
                                    break;

                                case 'X':
                                    settings.QueryReplace = true;
                                    break;

                                case 'z':
                                    settings.ZoobankCloneXml = true;
                                    break;

                                case 'r':
                                    settings.ParseReferences = true;
                                    break;

                                case 'R':
                                    settings.TagReferences = true;
                                    break;

                                case 'Q':
                                    settings.QuentinSpecificActions = true;
                                    break;

                                case 'F':
                                    settings.Flora = true;
                                    break;

                                case 's':
                                    settings.ParseBySection = true;
                                    break;

                                case 'V':
                                    settings.ValidateTaxa = true;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}