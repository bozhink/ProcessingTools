// <copyright file="IPresenter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Presenter.
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Gets the user context.
        /// </summary>
        /// <returns>Current user context.</returns>
        Task<UserContext> GetUserContextAsync();
    }
}
