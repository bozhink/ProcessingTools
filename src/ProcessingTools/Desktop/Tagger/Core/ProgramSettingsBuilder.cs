namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ProcessingTools.Commands.Tagger;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public class ProgramSettingsBuilder
    {
        private readonly ILogger logger;
        private readonly ICommandInfoProvider commandInfoProvider;

        public ProgramSettingsBuilder(ILogger logger, string[] args)
        {
            this.logger = logger;

            this.commandInfoProvider = new CommandInfoProvider();
            this.commandInfoProvider.ProcessInformation();

            this.Settings = new ProgramSettings();

            this.ParseFileNames(args);
            this.ParseSingleDashedOptions(args);
            this.ParseDoubleDashedOptions(args);
            this.ParseDirectCommandCalls(args);
        }

        public ProgramSettings Settings { get; private set; }

        private void ParseFileNames(string[] args)
        {
            Regex matchNonOptions = new Regex(@"\A[^/\-\+]");

            var arguments = args.Where(a => matchNonOptions.IsMatch(a)).ToArray();

            if (!arguments.Any())
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
            IEnumerable<string> oprions = new HashSet<string>(args.Where(a => matchSingleDashedOption.IsMatch(a)));

            foreach (string option in oprions)
            {
                char[] arg = option.ToCharArray();
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

                        case 'M':
                            this.Settings.MergeInputFiles = true;
                            break;

                        case 'S':
                            this.Settings.SplitDocument = true;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void ParseDoubleDashedOptions(string[] args)
        {
            Regex matchDoubleDashedOption = new Regex(@"\A\-\-");
            IEnumerable<string> options = new HashSet<string>(args.Where(a => matchDoubleDashedOption.IsMatch(a)));

            foreach (string option in options)
            {
                switch (option)
                {
                    case "--split-aphia":
                        this.Settings.ParseHigherWithAphia = true;
                        break;

                    case "--split-col":
                        this.Settings.ParseHigherWithCoL = true;
                        break;

                    case "--split-gbif":
                        this.Settings.ParseHigherWithGbif = true;
                        break;

                    case "--split-suffix":
                        this.Settings.ParseHigherBySuffix = true;
                        break;

                    case "--above-genus":
                        this.Settings.ParseHigherAboveGenus = true;
                        break;

                    case "--system":
                        this.Settings.ArticleSchemaType = SchemaType.System;
                        break;

                    case "--nlm":
                        this.Settings.ArticleSchemaType = SchemaType.Nlm;
                        break;

                    case "--extract-taxa":
                    case "--et":
                        this.Settings.ExtractTaxa = true;
                        break;

                    case "--extract-lower-taxa":
                    case "--elt":
                        this.Settings.ExtractLowerTaxa = true;
                        break;

                    case "--extract-higher-taxa":
                    case "--eht":
                        this.Settings.ExtractHigherTaxa = true;
                        break;

                    case "--zoobank-nlm":
                        this.Settings.ZoobankGenerateRegistrationXml = true;
                        break;

                    case "--zoobank-json":
                        this.Settings.ZoobankCloneJson = true;
                        break;

                    case "--zoobank-clone":
                        this.Settings.ZoobankCloneXml = true;
                        break;

                    case "--parse-treatment-meta-with-aphia":
                    case "--ptm-aphia":
                        this.Settings.ParseTreatmentMetaWithAphia = true;
                        break;

                    case "--parse-treatment-meta-with-gbif":
                    case "--ptm-gbif":
                        this.Settings.ParseTreatmentMetaWithGbif = true;
                        break;

                    case "--parse-treatment-meta-with-col":
                    case "--ptm-col":
                        this.Settings.ParseTreatmentMetaWithCol = true;
                        break;

                    case "--table-fn":
                        this.Settings.TagTableFn = true;
                        break;

                    case "--environments":
                        this.Settings.TagEnvironmentTerms = true;
                        break;

                    case "--codes":
                        this.Settings.TagCodes = true;
                        break;

                    case "--abbrev":
                        this.Settings.TagAbbreviations = true;
                        break;

                    case "--envo":
                        this.Settings.TagEnvironmentTermsWithExtract = true;
                        break;

                    case "--xsl":
                        this.Settings.RunXslTransform = true;
                        break;

                    case "--merge":
                        this.Settings.MergeInputFiles = true;
                        break;

                    case "--split":
                        this.Settings.SplitDocument = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void ParseDirectCommandCalls(string[] args)
        {
            Regex matchDirectCall = new Regex(@"\A\+\w+");

            var commandNames = args
                .Where(a => matchDirectCall.IsMatch(a))
                .Select(a => a.Substring(1))
                .ToArray();

            foreach (var commandName in commandNames)
            {
                string commandNameLowerCase = commandName.ToLowerInvariant();

                var matchingCommands = this.commandInfoProvider
                    .CommandsInformation
                    .Where(i => i.Value.Name.ToLowerInvariant().StartsWith(commandNameLowerCase))
                    .ToArray();

                switch (matchingCommands.Length)
                {
                    case 0:
                        {
                            this.logger?.Log(LogType.Warning, "No matching command '{0}'.", commandName);
                        }

                        break;

                    case 1:
                        {
                            var commandInfo = matchingCommands.Single().Value;
                            this.Settings.CalledCommands.Add(commandInfo.CommandType);
                        }

                        break;

                    default:
                        {
                            // Get direct full-name match from a list of ‘like’-matches
                            var commandInfo = matchingCommands.Select(c => c.Value).FirstOrDefault(c => c.Name.ToLowerInvariant() == commandNameLowerCase);
                            if (commandInfo != null)
                            {
                                this.Settings.CalledCommands.Add(commandInfo.CommandType);
                            }
                            else
                            {
                                this.logger?.Log(
                                    LogType.Warning,
                                    "Multiple commands match input name '{0}': {1}",
                                    commandName,
                                    string.Join("\n\t", matchingCommands.Select(c => c.Key.ToString())));
                            }
                        }

                        break;
                }
            }
        }

        private void PrintHelp()
        {
            this.logger?.Log(message: Messages.HelpMessage);

            // Print commands’ information
            foreach (var commandType in this.commandInfoProvider.CommandsInformation.Keys.OrderBy(k => k.Name))
            {
                var commandInfo = this.commandInfoProvider.CommandsInformation[commandType];
                this.logger?.Log("    +{0}\t=\t{1}", commandInfo.Name, commandInfo.Description);
            }

            Environment.Exit(1);
        }
    }
}
