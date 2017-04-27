namespace ProcessingTools.Processors.Contracts.Processors
{
    using System.Threading.Tasks;

    public interface IQueryReplacer
    {
        /// <summary>
        /// Does multiple replaces using a valid xml query.
        /// </summary>
        /// <param name="content">Content to be processed.</param>
        /// <param name="queryFilePath">Valid Xml file containing [multiple ]replace instructions.</param>
        /// <returns>Task of the processed content.</returns>
        Task<string> Replace(string content, string queryFilePath);
    }
}
