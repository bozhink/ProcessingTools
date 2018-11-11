// <copyright file="Constants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Integration.Tests
{
    /// <summary>
    /// Test constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        internal const string ConnectionString = "redis://localhost:6379?ssl=false&client=ProcessingToolsCache&db=1&connectTimeout=1000&sendTimeout=100&receiveTimeout=100&idleTimeoutSecs=4";

        /// <summary>
        /// Client field name.
        /// </summary>
        internal const string ClientFieldName = "client";

        /// <summary>
        /// Client property name.
        /// </summary>
        internal const string ClientPropertyName = "Client";

        /// <summary>
        /// Key parameter name.
        /// </summary>
        internal const string KeyParamName = "key";

        /// <summary>
        /// Value parameter name.
        /// </summary>
        internal const string ValueParamName = "value";
    }
}
