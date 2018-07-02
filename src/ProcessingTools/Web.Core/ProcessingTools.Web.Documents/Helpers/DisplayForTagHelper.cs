// <copyright file="DisplayForTagHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Helpers
{
    using System;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// DisplayFor tag helper.
    /// See https://stackoverflow.com/questions/32671644/mvc6-alternative-to-html-displayfor
    /// </summary>
    [HtmlTargetElement("p", Attributes = DisplayForAttributeName)]
    [HtmlTargetElement("div", Attributes = DisplayForAttributeName)]
    [HtmlTargetElement("span", Attributes = DisplayForAttributeName)]
    public class DisplayForTagHelper : TagHelper
    {
        private const string DisplayForAttributeName = "asp-display-for";

        /// <summary>
        /// Gets or sets the For model expression.
        /// </summary>
        [HtmlAttributeName(DisplayForAttributeName)]
        public ModelExpression DisplayFor { get; set; }

        /// <inheritdoc/>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var text = this.DisplayFor.ModelExplorer.GetSimpleDisplayText();

            output.Content.SetContent(text);
        }
    }
}
