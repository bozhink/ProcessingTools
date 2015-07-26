using System;
using System.Collections.Generic;

namespace ProcessingTools.Tag
{
    public partial class Tagger
    {
        private const int NumberOfExpandingIterations = 1;
        private static bool tagWWW = false;
        private static bool tagDoi = false;
        private static bool tagFigTab = false;
        private static bool tagTableFn = false;
        private static bool tagCoords = false;
        private static bool parseCoords = false;
        private static bool formatInit = false;
        private static bool formatTreat = false;
        private static bool taxaA = false;
        private static bool taxaB = false;
        private static bool taxaC = false;
        private static bool taxaD = false;
        private static bool taxaE = false;
        private static bool extractTaxa = false;
        private static bool extractLowerTaxa = false;
        private static bool extractHigherTaxa = false;
        private static bool untagSplit = false;
        private static bool flag1 = false;
        private static bool flag2 = false;
        private static bool flag3 = false;
        private static bool flag4 = false;
        private static bool flag5 = false;
        private static bool flag6 = false;
        private static bool flag7 = false;
        private static bool flag8 = false;
        private static bool nlm = false;
        private static bool html = false;
        private static bool zoobank = false;
        private static bool zoobankJson = false;
        private static bool generateZooBankNlm = false;
        private static bool parseReferences = false;
        private static bool tagReferences = false;
        private static bool quentinSpecificActions = false;
        private static bool queryReplace = false;
        private static bool flora = false;
        private static bool parseBySection = false;
        private static bool splitHigherBySuffix = false;
        private static bool splitHigherWithGbif = false;
        private static bool splitHigherWithAphia = false;
        private static bool splitHigherWithCoL = false;
        private static bool splitHigherAboveGenus = false;
        private static bool parseTreatmentMetaWithAphia = false;
        private static bool parseTreatmentMetaWithGbif = false;
        private static bool parseTreatmentMetaWithCol = false;
        private static bool testFlag = false;
        private static bool normalizeFlag = false;
        private static bool tagEnvironments = false;
        private static bool tagCodes = false;
        private static bool tagQuantities = false;
        private static bool tagDates = false;
        private static bool tagAbbrev = false;
        private static bool tagEnvo = false;
        private static Config config;

        private static string higherStructrureXpath = "//article"; // "//sec[name(..)!='sec']";

        private static List<int> singleDashedOptions = new List<int>();
        private static List<int> doubleDashedOptions = new List<int>();
        private static List<int> arguments = new List<int>();

        private static string inputFileName = string.Empty;
        private static string outputFileName = string.Empty;
        private static string queryFileName = string.Empty;

        private static void ParseDoubleDashedOptions(string[] args)
        {
            foreach (int item in doubleDashedOptions)
            {
                if (args[item].CompareTo("--split-aphia") == 0)
                {
                    splitHigherWithAphia = true;
                }
                else if (args[item].CompareTo("--split-col") == 0)
                {
                    splitHigherWithCoL = true;
                }
                else if (args[item].CompareTo("--split-gbif") == 0)
                {
                    splitHigherWithGbif = true;
                }
                else if (args[item].CompareTo("--split-suffix") == 0)
                {
                    splitHigherBySuffix = true;
                }
                else if (args[item].CompareTo("--above-genus") == 0)
                {
                    splitHigherAboveGenus = true;
                }
                else if (args[item].CompareTo("--system") == 0)
                {
                    config.NlmStyle = false;
                }
                else if (args[item].CompareTo("--normalize") == 0)
                {
                    normalizeFlag = true;
                }
                else if (args[item].CompareTo("--test") == 0)
                {
                    testFlag = true;
                }
                else if (args[item].CompareTo("--extract-taxa") == 0 || args[item].CompareTo("--et") == 0)
                {
                    extractTaxa = true;
                }
                else if (args[item].CompareTo("--extract-lower-taxa") == 0 || args[item].CompareTo("--elt") == 0)
                {
                    extractLowerTaxa = true;
                }
                else if (args[item].CompareTo("--extract-higher-taxa") == 0 || args[item].CompareTo("--eht") == 0)
                {
                    extractHigherTaxa = true;
                }
                else if (args[item].CompareTo("--zoobank-nlm") == 0)
                {
                    generateZooBankNlm = true;
                }
                else if (args[item].CompareTo("--zoobank-json") == 0)
                {
                    zoobankJson = true;
                }
                else if (args[item].CompareTo("--zoobank-clone") == 0)
                {
                    zoobank = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-aphia") == 0 || args[item].CompareTo("--ptm-aphia") == 0)
                {
                    parseTreatmentMetaWithAphia = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-gbif") == 0 || args[item].CompareTo("--ptm-gbif") == 0)
                {
                    parseTreatmentMetaWithGbif = true;
                }
                else if (args[item].CompareTo("--parse-treatment-meta-with-col") == 0 || args[item].CompareTo("--ptm-col") == 0)
                {
                    parseTreatmentMetaWithCol = true;
                }
                else if (args[item].CompareTo("--table-fn") == 0)
                {
                    tagTableFn = true;
                }
                else if (args[item].CompareTo("--environments") == 0)
                {
                    tagEnvironments = true;
                }
                else if (args[item].CompareTo("--codes") == 0)
                {
                    tagCodes = true;
                }
                else if (args[item].CompareTo("--quantities") == 0)
                {
                    tagQuantities = true;
                }
                else if (args[item].CompareTo("--dates") == 0)
                {
                    tagDates = true;
                }
                else if (args[item].CompareTo("--abbrev") == 0)
                {
                    tagAbbrev = true;
                }
                else if (args[item].CompareTo("--envo") == 0)
                {
                    tagEnvo = true;
                }
            }
        }

        private static void ParseSingleDashedOptions(string[] args)
        {
            foreach (int item in singleDashedOptions)
            {
                char[] arg = args[item].ToCharArray();
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
                            formatInit = true;
                            break;
                        case 't':
                            formatTreat = true;
                            break;
                        case 'A':
                            taxaA = true;
                            break;
                        case 'B':
                            taxaB = true;
                            break;
                        case 'C':
                            taxaC = true;
                            break;
                        case 'D':
                            taxaD = true;
                            break;
                        case 'E':
                            taxaE = true;
                            break;
                        case 'u':
                            untagSplit = true;
                            break;
                        case 'w':
                            tagWWW = true;
                            break;
                        case 'd':
                            tagDoi = true;
                            break;
                        case 'f':
                            tagFigTab = true;
                            break;
                        case 'c':
                            tagCoords = true;
                            break;
                        case 'k':
                            parseCoords = true;
                            break;
                        case '1':
                            flag1 = true;
                            break;
                        case '2':
                            flag2 = true;
                            break;
                        case '3':
                            flag3 = true;
                            break;
                        case '4':
                            flag4 = true;
                            break;
                        case '5':
                            flag5 = true;
                            break;
                        case '6':
                            flag6 = true;
                            break;
                        case '7':
                            flag7 = true;
                            break;
                        case '8':
                            flag8 = true;
                            break;
                        case 'X':
                            queryReplace = true;
                            break;
                        case 'N':
                            nlm = true;
                            break;
                        case 'H':
                            html = true;
                            break;
                        case 'z':
                            zoobank = true;
                            break;
                        case 'r':
                            parseReferences = true;
                            break;
                        case 'R':
                            tagReferences = true;
                            break;
                        case 'Q':
                            quentinSpecificActions = true;
                            break;
                        case 'F':
                            flora = true;
                            break;
                        case 's':
                            parseBySection = true;
                            break;
                    }
                }
            }
        }
    }
}
