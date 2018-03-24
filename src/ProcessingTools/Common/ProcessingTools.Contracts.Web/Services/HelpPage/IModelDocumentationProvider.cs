// <copyright file="IModelDocumentationProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.HelpPage
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides model descriptions for given types.
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation based on <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The member</param>
        /// <returns>Documentation as string</returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation based on <see cref="Type"/>
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>Documentation as string</returns>
        string GetDocumentation(Type type);
    }
}
