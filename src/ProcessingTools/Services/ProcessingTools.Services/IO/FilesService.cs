﻿// <copyright file="FilesService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.IO;

namespace ProcessingTools.Services.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// Files service.
    /// </summary>
    public class FilesService : IFilesService
    {
        /// <inheritdoc/>
        public void CopyFile(string sourceFileName, string destinationFileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(string sourceFileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(Stream sourceStream)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(string sourceFileName, int maximalFileNameLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(Stream sourceStream, int maximalFileNameLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(string sourceFileName, string baseFileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(Stream sourceStream, string baseFileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(string sourceFileName, string baseFileName, int maximalFileNameLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string CopyToNewFile(Stream sourceStream, string baseFileName, int maximalFileNameLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetFileFullName(string fileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetFilesDirectoryFullName()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetNewFileName()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetNewFileName(int maximalLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetNewFileName(string fileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetNewFileName(string fileName, int maximalLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetTempDirectoryFullName()
        {
            throw new System.NotImplementedException();
        }
    }
}
