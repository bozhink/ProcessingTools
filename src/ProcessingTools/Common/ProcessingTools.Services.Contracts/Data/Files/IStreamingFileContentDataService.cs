// <copyright file="IStreamingFileContentDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IStreamingFileContentDataService
    {
        StreamReader GetReader(object id);

        Stream ReadToStream(object id);

        Task<object> Write(object id, Stream stream);

        Task<object> Write(object id, StreamReader streamReader);
    }
}
