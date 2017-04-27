namespace ProcessingTools.Tagger.Commands.Contracts
{
    using System.Collections.Generic;

    public interface ICommandSettings
    {
        bool ExtractHigherTaxa { get; }

        bool ExtractLowerTaxa { get; }

        bool ExtractTaxa { get; }

        IList<string> FileNames { get; }

        string OutputFileName { get; set; }
    }
}
