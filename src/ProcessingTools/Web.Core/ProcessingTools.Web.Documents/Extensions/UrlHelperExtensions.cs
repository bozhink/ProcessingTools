// <copyright file="UrlHelperExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace Microsoft.AspNetCore.Mvc
{
    using ProcessingTools.Web.Documents.Controllers;

    /// <summary>
    /// <see cref="IUrlHelper"/> extensions.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates e-mail confirmation link.
        /// </summary>
        /// <param name="urlHelper"><see cref="IUrlHelper"/></param>
        /// <param name="userId">User ID.</param>
        /// <param name="code">Code to be send.</param>
        /// <param name="scheme">Protocol scheme.</param>
        /// <returns>Confirmation link as string.</returns>
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: AccountController.ConfirmEmailActionName,
                controller: AccountController.ControllerName,
                values: new { userId, code },
                protocol: scheme);
        }

        /// <summary>
        /// Reset password callback link handler.
        /// </summary>
        /// <param name="urlHelper"><see cref="IUrlHelper"/></param>
        /// <param name="userId">User ID.</param>
        /// <param name="code">Reset code.</param>
        /// <param name="scheme">Protocol scheme.</param>
        /// <returns>Reset password response as string.</returns>
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: AccountController.ResetPasswordActionName,
                controller: AccountController.ControllerName,
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
