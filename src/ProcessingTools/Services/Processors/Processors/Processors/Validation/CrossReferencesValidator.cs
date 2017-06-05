namespace ProcessingTools.Processors.Processors.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Validation;

    public class CrossReferencesValidator : ICrossReferencesValidator
    {
        public Task<object> Validate(IDocument document, IReporter reporter)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            var histogram = this.GetHistogramOfIdValues(document);

            var nonUniqueIds = histogram.Where(p => p.Value > 1L).Select(p => p.Key).ToList();
            if (nonUniqueIds.Count > 0)
            {
                reporter.AppendContent(string.Format("Duplicated ID definitions: {0}", string.Join(", ", nonUniqueIds)));
            }

            var invalidReferences = this.GetInvalidReferences(document, histogram).ToList();
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
                    long number = 0L;
                    if (histogram.TryGetValue(id, out number))
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
