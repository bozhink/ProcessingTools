// <copyright file="IWebPresenter.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Web.Models.Shared;

namespace ProcessingTools.Contracts.Web.Services
{
    /// <summary>
    /// Presenter.
    /// </summary>
    public interface IWebPresenter
    {
        /// <summary>
        /// Gets the user context.
        /// </summary>
        /// <returns>Current user context.</returns>
        Task<UserContext> GetUserContextAsync();
    }
}
