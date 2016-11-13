﻿namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;

    public interface IFileProcessor
    {
        string InputFileName { get; set; }

        string OutputFileName { get; set; }

        string QueryFileName { get; set; }

        Task Run(IProgramSettings settings);
    }
}