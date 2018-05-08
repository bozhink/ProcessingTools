// <copyright file="ManageNavPages.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Views.Manage
{
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    /// <summary>
    /// Manage navigation pages.
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Gets "ActivePage".
        /// </summary>
        public static string ActivePageKey => "ActivePage";

        /// <summary>
        /// Gets "Index".
        /// </summary>
        public static string Index => "Index";

        /// <summary>
        /// Gets "ChangePassword".
        /// </summary>
        public static string ChangePassword => "ChangePassword";

        /// <summary>
        /// Gets "ExternalLogins".
        /// </summary>
        public static string ExternalLogins => "ExternalLogins";

        /// <summary>
        /// Gets "TwoFactorAuthentication".
        /// </summary>
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        /// <summary>
        /// Gets IndexNavClass.
        /// </summary>
        /// <param name="viewContext">View context.</param>
        /// <returns>Class name.</returns>
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        /// <summary>
        /// Gets ChangePasswordNavClass.
        /// </summary>
        /// <param name="viewContext">View context.</param>
        /// <returns>Class name.</returns>
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        /// <summary>
        /// Gets ExternalLoginsNavClass.
        /// </summary>
        /// <param name="viewContext">View context.</param>
        /// <returns>Class name.</returns>
        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        /// <summary>
        /// Gets TwoFactorAuthenticationNavClass.
        /// </summary>
        /// <param name="viewContext">View context.</param>
        /// <returns>Class name.</returns>
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        /// <summary>
        /// Gets PageNavClass.
        /// </summary>
        /// <param name="viewContext">View context.</param>
        /// <param name="page">Page name.</param>
        /// <returns>Class name.</returns>
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        /// <summary>
        /// Adds active page to viewData.
        /// </summary>
        /// <param name="viewData">ViewData dictionary.</param>
        /// <param name="activePage">Active page name.</param>
        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
