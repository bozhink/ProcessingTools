namespace ProcessingTools.Layout.Processors.Contracts.Normalizers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Provides normalizations for IDocument objects.
    /// </summary>
    public interface IDocumentNormalizer
    {
        /// <summary>
        /// Normalizes the IDocument object's xml content to the system schema.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns></returns>
        Task<object> NormalizeToSystem(IDocument document);

        /// <summary>
        /// Normalizes the IDocument object's xml to its current SchemaType.
        /// </summary>
        /// <param name="document">IDocument object to be normalized.</param>
        /// <returns></returns>
        Task<object> NormalizeToDocumentSchema(IDocument document);
    }
}
