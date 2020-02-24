﻿// <copyright file="CrossReferencesValidator.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Validation;

    /// <summary>
    /// Cross references validator.
    /// </summary>
    public class CrossReferencesValidator : ICrossReferencesValidator
    {
        /// <inheritdoc/>
        public Task<object> ValidateAsync(IDocument context, IReporter reporter)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (reporter is null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            var histogram = this.GetHistogramOfIdValues(context);

            var nonUniqueIds = histogram.Where(p => p.Value > 1L).Select(p => p.Key).ToList();
            if (nonUniqueIds.Count > 0)
            {
                reporter.AppendContent(string.Format("Duplicated ID definitions: {0}", string.Join(", ", nonUniqueIds)));
            }

            var invalidReferences = this.GetInvalidReferences(context, histogram).ToList();
            if (invalidReferences.Count > 0)
            {
                reporter.AppendContent(string.Format("Invalid ID references: {0}", string.Join(", ", invalidReferences)));
            }

            return Task.FromResult<object>(true);
        }

        private IEnumerable<string> GetInvalidReferences(IDocument document, IDictionary<string, long> histogram)
        {
            var invalidReferences = new HashSet<string>();
            document.SelectNodes(XPathStrings.RidAttributes)
                .Select(n => n.InnerText.Trim())
                .ToList()
                .ForEach(rid =>
                {
                    if (!histogram.ContainsKey(rid))
                    {
                        invalidReferences.Add(rid);
                    }
                });
            return invalidReferences;
        }

        private IDictionary<string, long> GetHistogramOfIdValues(IDocument document)
        {
            var histogram = new Dictionary<string, long>();
            document.SelectNodes(XPathStrings.IdAttributes)
                .Select(n => n.InnerText.Trim())
                .ToList()
                .ForEach(id =>
                {
                    if (histogram.TryGetValue(id, out long number))
                    {
                        histogram[id] = number + 1;
                    }
                    else
                    {
                        histogram.Add(id, 1L);
                    }
                });

            return histogram;
        }
    }
}
