namespace ProcessingTools.Tagger.Commands
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Commands;

    public class CommandSettings : ICommandSettings
    {
        public CommandSettings()
        {
            this.FileNames = new List<string>();
        }

        public bool ExtractHigherTaxa { get; set; } = false;

        public bool ExtractLowerTaxa { get; set; } = false;

        public bool ExtractTaxa { get; set; } = false;

        public IList<string> FileNames { get; set; }

        public string OutputFileName { get; set; }
    }
}
