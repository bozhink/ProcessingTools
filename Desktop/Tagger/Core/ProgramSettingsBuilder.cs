namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class ProgramSettingsBuilder
    {
        private ILogger logger;
        private ControllerInfoProvider controllerInfoProvider;

        public ProgramSettingsBuilder(ILogger logger, string[] args)
        {
            this.logger = logger;

            this.controllerInfoProvider = new ControllerInfoProvider();
            this.controllerInfoProvider.ProcessInformation();

            this.Settings = new ProgramSettings();

            this.ParseFileNames(args);
            this.ParseSingleDashedOptions(args);
            this.ParseDoubleDashedOptions(args);
            this.ParseDirectControllerCalls(args);
        }

        public ProgramSettings Settings { get; private set; }

        private void ParseFileNames(string[] args)
        {
            Regex matchNonOptions = new Regex(@"\A[^/\-\+]");

            var arguments = args.Where(a => matchNonOptions.IsMatch(a));

            if (arguments.Count() < 1)
            {
                this.PrintHelp();
            }

            foreach (var argument in arguments)
            {
                this.Settings.FileNames.Add(argument);
            }
        }

        private void ParseSingleDashedOptions(string[] args)
        {
            Regex matchSingleDashedOption = new Regex(@"\A\-[^\-]|\A/[^/]");

            foreach (string item in args.Where(a => matchSingleDashedOption.IsMatch(a)))
            {
                char[] arg = item.ToCharArray();
                int length = arg.Length;
                if (length < 2)
                {
                    continue;
                }

                for (int i = 1; i < length; ++i)
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

                        case 'V':
                            this.Settings.ValidateTaxa = true;
                            break;
                    }
                }
            }
        }

        private void ParseDoubleDashedOptions(string[] args)
        {
            Regex matchDoubleDashedOption = new Regex(@"\A\-\-");
            foreach (string item in args.Where(a => matchDoubleDashedOption.IsMatch(a)))
            {
                if (item.CompareTo("--split-aphia") == 0)
                {
                    this.Settings.ParseHigherWithAphia = true;
                }
                else if (item.CompareTo("--split-col") == 0)
                {
                    this.Settings.ParseHigherWithCoL = true;
                }
                else if (item.CompareTo("--split-gbif") == 0)
                {
                    this.Settings.ParseHigherWithGbif = true;
                }
                else if (item.CompareTo("--split-suffix") == 0)
                {
                    this.Settings.ParseHigherBySuffix = true;
                }
                else if (item.CompareTo("--above-genus") == 0)
                {
                    this.Settings.ParseHigherAboveGenus = true;
                }
                else if (item.CompareTo("--system") == 0)
                {
                    this.Settings.ArticleSchemaType = SchemaType.System;
                }
                else if (item.CompareTo("--nlm") == 0)
                {
                    this.Settings.ArticleSchemaType = SchemaType.Nlm;
                }
                else if (item.CompareTo("--extract-taxa") == 0 || item.CompareTo("--et") == 0)
                {
                    this.Settings.ExtractTaxa = true;
                }
                else if (item.CompareTo("--extract-lower-taxa") == 0 || item.CompareTo("--elt") == 0)
                {
                    this.Settings.ExtractLowerTaxa = true;
                }
                else if (item.CompareTo("--extract-higher-taxa") == 0 || item.CompareTo("--eht") == 0)
                {
                    this.Settings.ExtractHigherTaxa = true;
                }
                else if (item.CompareTo("--zoobank-nlm") == 0)
                {
                    this.Settings.ZoobankGenerateRegistrationXml = true;
                }
                else if (item.CompareTo("--zoobank-json") == 0)
                {
                    this.Settings.ZoobankCloneJson = true;
                }
                else if (item.CompareTo("--zoobank-clone") == 0)
                {
                    this.Settings.ZoobankCloneXml = true;
                }
                else if (item.CompareTo("--parse-treatment-meta-with-aphia") == 0 || item.CompareTo("--ptm-aphia") == 0)
                {
                    this.Settings.ParseTreatmentMetaWithAphia = true;
                }
                else if (item.CompareTo("--parse-treatment-meta-with-gbif") == 0 || item.CompareTo("--ptm-gbif") == 0)
                {
                    this.Settings.ParseTreatmentMetaWithGbif = true;
                }
                else if (item.CompareTo("--parse-treatment-meta-with-col") == 0 || item.CompareTo("--ptm-col") == 0)
                {
                    this.Settings.ParseTreatmentMetaWithCol = true;
                }
                else if (item.CompareTo("--table-fn") == 0)
                {
                    this.Settings.TagTableFn = true;
                }
                else if (item.CompareTo("--environments") == 0)
                {
                    this.Settings.TagEnvironmentTerms = true;
                }
                else if (item.CompareTo("--codes") == 0)
                {
                    this.Settings.TagCodes = true;
                }
                else if (item.CompareTo("--abbrev") == 0)
                {
                    this.Settings.TagAbbreviations = true;
                }
                else if (item.CompareTo("--envo") == 0)
                {
                    this.Settings.TagEnvironmentTermsWithExtract = true;
                }
                else if (item.CompareTo("--validate-taxa") == 0)
                {
                    this.Settings.ValidateTaxa = true;
                }
                else if (item.CompareTo("--xsl") == 0)
                {
                    this.Settings.RunXslTransform = true;
                }
            }
        }

        private void ParseDirectControllerCalls(string[] args)
        {
            Regex matchDirectControllerCall = new Regex(@"\A\+\w+");

            var controllerNames = args
                .Where(a => matchDirectControllerCall.IsMatch(a))
                .Select(a => a.Substring(1))
                .ToArray();

            foreach (var controllerName in controllerNames)
            {
                var matchingControllers = this.controllerInfoProvider
                    .ControllersInformation
                    .Where(i => i.Value.Name.ToLower().IndexOf(controllerName.ToLower()) == 0)
                    .ToArray();

                switch (matchingControllers.Length)
                {
                    case 0:
                        this.logger?.Log(LogType.Warning, "No matching controller '{0}'.", controllerName);
                        break;

                    case 1:
                        var controllerInfo = matchingControllers.Single().Value;
                        this.Settings.CalledControllers.Add(controllerInfo.ControllerType);
                        break;

                    default:
                        this.logger?.Log(
                            LogType.Warning,
                            "Multiple controllers match input name '{0}': {1}",
                            controllerName,
                            string.Join("\n\t", matchingControllers.Select(c => c.Key.ToString())));
                        break;
                }
            }
        }

        private void PrintHelp()
        {
            this.logger?.Log(Messages.HelpMessage);

            // Print controllers’ information
            foreach (var contollerType in this.controllerInfoProvider.ControllersInformation.Keys.OrderBy(k => k.Name))
            {
                var controllerInfo = this.controllerInfoProvider.ControllersInformation[contollerType];
                this.logger?.Log("    +{0}\t=\t{1}", controllerInfo.Name, controllerInfo.Description);
            }

            Environment.Exit(1);
        }
    }
}
