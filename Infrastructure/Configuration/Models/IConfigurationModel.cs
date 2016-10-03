namespace ProcessingTools.Configuration.Models
{
    using System.Collections.Generic;

    public interface IConfigurationModel
    {
        IEnumerable<IJsonKeyValuePair> Files { get; }

        IEnumerable<IJsonKeyValuePair> Settings { get; }
    }
}
