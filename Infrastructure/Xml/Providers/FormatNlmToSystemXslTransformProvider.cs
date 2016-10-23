﻿namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Abstracts;
    using Contracts.Cache;
    using Contracts.Providers;

    using ProcessingTools.Constants.Configuration;

    public class FormatNlmToSystemXslTransformProvider : AbstractXslTransformProvider, IFormatNlmToSystemXslTransformProvider
    {
        public FormatNlmToSystemXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslPathKey];
    }
}
