// <copyright file="IArticleInsertModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article insert model.
    /// </summary>
    public interface IArticleInsertModel
    {
        string Title { get; set; }

        string SubTitle { get; set; }

        string VolumeSeries { get; set; }

        string Volume { get; set; }

        string Issue { get; set; }

        string IssuePart { get; set; }

        string ELocationId { get; set; }

        string JournalId { get; set; }

        string FirstPage { get; set; }

        string LastPage { get; set; }

        int NumberOfPages { get; set; }
    }
}
