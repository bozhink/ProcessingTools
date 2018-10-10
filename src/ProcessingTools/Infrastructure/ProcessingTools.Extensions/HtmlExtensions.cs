﻿// <copyright file="HtmlExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

// See https://github.com/RickStrahl/Westwind.Utilities/blob/master/Westwind.Utilities/Utilities/HtmlUtils.cs
namespace ProcessingTools.Extensions
{
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// HTML extensions.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Replaces &lt; and &gt; and Quote characters to HTML safe equivalents.
        /// </summary>
        /// <param name="html">HTML to convert.</param>
        /// <returns>Returns an HTML string of the converted text.</returns>
        public static string FixHTMLForDisplay(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return html;
            }

            return html
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;");
        }

        /// <summary>
        /// Strips HTML tags out of an HTML string and returns just the text.
        /// </summary>
        /// <param name="html">HTML to convert.</param>
        /// <returns>Fixed up string.</returns>
        public static string StripHtml(string html)
        {
            string text = html ?? string.Empty;
            text = Regex.Replace(text ?? string.Empty, @"<[^>]*?>|[\r\n]", string.Empty);
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }

        /// <summary>
        /// Fixes a plain text field for display as HTML by replacing carriage returns with the appropriate br tags for breaks.
        /// </summary>
        /// <param name="text">Input string.</param>
        /// <returns>Fixed up string.</returns>
        public static string DisplayMemo(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "<br />\r\n");
            return text;
        }

        /// <summary>
        /// Method that handles display of text by breaking text.
        /// Unlike the non-encoded version it encodes any embedded HTML text
        /// </summary>
        /// <param name="text">Input string.</param>
        /// <returns>Fixed up string.</returns>
        public static string DisplayMemoEncoded(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            bool preTag = false;
            if (text.Contains("<pre>"))
            {
                text = text.Replace("<pre>", "__pre__");
                text = text.Replace("</pre>", "__/pre__");
                preTag = true;
            }

            // Fix up line breaks into <br><p>
            text = DisplayMemo(HtmlEncode(text));

            if (preTag)
            {
                text = text.Replace("__pre__", "<pre>");
                text = text.Replace("__/pre__", "</pre>");
            }

            return text;
        }

        /// <summary>
        /// HTML-encodes a string and returns the encoded string.
        /// </summary>
        /// <param name="text">The text string to encode.</param>
        /// <returns>The HTML-encoded text.</returns>
        public static string HtmlEncode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {
                    case '<':
                        sb.Append("&lt;");
                        break;

                    case '>':
                        sb.Append("&gt;");
                        break;

                    case '"':
                        sb.Append("&quot;");
                        break;

                    case '&':
                        sb.Append("&amp;");
                        break;

                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                        {
                            sb.Append(text[i]);
                        }

                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Create an Href HTML link.
        /// </summary>
        /// <param name="text">Display text of the HTML link.</param>
        /// <param name="url">URL of the HTML link.</param>
        /// <param name="target">Value of the target attribute.</param>
        /// <param name="additionalMarkup">Any additional attributes added to the element.</param>
        /// <returns>HTML link as string.</returns>
        public static string Href(string text, string url, string target, string additionalMarkup)
        {
            return $@"<a href=""{url}""" +
                (string.IsNullOrWhiteSpace(target) ? string.Empty : $@" target=""{target}""") +
                (string.IsNullOrWhiteSpace(additionalMarkup) ? string.Empty : $@" {additionalMarkup}") +
                $">{text}</a>";
        }

        /// <summary>
        /// Create an Href HTML link.
        /// </summary>
        /// <param name="url">URL of the HTML link.</param>
        /// /// <returns>HTML link as string.</returns>
        public static string Href(string url)
        {
            return Href(url, url, null, null);
        }

        /// <summary>
        /// Returns an IMG link as a string. If the image is null or empty a blank string is returned.
        /// </summary>
        /// <param name="imageUrl">URL of the image file.</param>
        /// <param name="additionalMarkup">Any additional attributes added to the element.</param>
        /// <returns>IMG link as a string.</returns>
        public static string ImgRef(string imageUrl, string additionalMarkup = null)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return string.Empty;
            }

            return $@"<img src=""{imageUrl}""" +
                (string.IsNullOrWhiteSpace(additionalMarkup) ? string.Empty : $@" {additionalMarkup}") +
                " />";
        }

        /// <summary>
        /// Sanitizes HTML to some of the most of
        /// </summary>
        /// <remarks>
        /// This provides rudimentary HTML sanitation catching the most obvious
        /// XSS script attack vectors. For more complete HTML Sanitation please look into
        /// a dedicated HTML Sanitizer.
        /// </remarks>
        /// <param name="html">input html</param>
        /// <param name="htmlTagBlacklist">A list of HTML tags that are stripped.</param>
        /// <returns>Sanitized HTML</returns>
        public static string SanitizeHtml(string html, string htmlTagBlacklist = "script|iframe|object|embed|form")
        {
            const string HtmlSanitizeTagBlackList = "script|iframe|object|embed|form";

            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            Regex regexScript = new Regex(
                $@"(<({HtmlSanitizeTagBlackList})\b[^<]*(?:(?!<\/({HtmlSanitizeTagBlackList}))<[^<]*)*<\/({HtmlSanitizeTagBlackList})>)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            // strip javascript: and unicode representation of javascript:
            // href='javascript:alert(\"gotcha\")'
            // href='&#106;&#97;&#118;&#97;&#115;&#99;&#114;&#105;&#112;&#116;:alert(\"gotcha\");'
            Regex regexJavaScriptHref = new Regex(
                @"<.*?\s(href|src|dynsrc|lowsrc)=.{0,20}((javascript:)|(&#)).*?>",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            Regex regexOnEventAttributes = new Regex(
                @"<[^>]*?\s(on[^\s\\]{0,20}=([""].*?[""]|['].*?['])).*?(>|\/>)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (!string.IsNullOrEmpty(htmlTagBlacklist) || htmlTagBlacklist == HtmlSanitizeTagBlackList)
            {
                html = regexScript.Replace(html, string.Empty);
            }
            else
            {
                html = Regex.Replace(
                    html,
                    $@"(<({htmlTagBlacklist})\b[^<]*(?:(?!<\/({HtmlSanitizeTagBlackList}))<[^<]*)*<\/({htmlTagBlacklist})>)",
                    string.Empty,
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }

            // Remove javascript: directives
            var matches = regexJavaScriptHref.Matches(html);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 2)
                {
                    var txt = match.Value.Replace(match.Groups[2].Value, "unsupported:");
                    html = html.Replace(match.Value, txt);
                }
            }

            // Remove onEvent handlers from elements
            matches = regexOnEventAttributes.Matches(html);
            foreach (Match match in matches)
            {
                var txt = match.Value;
                if (match.Groups.Count > 1)
                {
                    var onEvent = match.Groups[1].Value;
                    txt = txt.Replace(onEvent, string.Empty);
                    if (!string.IsNullOrEmpty(txt))
                    {
                        html = html.Replace(match.Value, txt);
                    }
                }
            }

            return html;
        }
    }
}
