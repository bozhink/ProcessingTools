// <copyright file="YamlSourceXmlReplaceRuleSetParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Rules
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Services.Contracts.Rules;
    using ProcessingTools.Services.Models.Rules;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// XML replace rule set parser for YAML source string.
    /// </summary>
    public class YamlSourceXmlReplaceRuleSetParser : IXmlReplaceRuleSetParser
    {
        /// <inheritdoc/>
        public async Task<IXmlReplaceRuleSetModel> ParseStringToRuleSetAsync(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            return await Task.Run(() =>
            {
                var reader = this.GetReader(source);
                var deserializer = this.GetDeserializer();
                var result = deserializer.Deserialize<XmlReplaceRuleSetModel>(reader);
                return result;
            }).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IXmlReplaceRuleSetModel[]> ParseStringToRuleSetsAsync(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return Array.Empty<IXmlReplaceRuleSetModel>();
            }

            return await Task.Run(() =>
            {
                var reader = this.GetReader(source);
                var deserializer = this.GetDeserializer();
                var result = deserializer.Deserialize<XmlReplaceRuleSetModel[]>(reader);
                return result;
            }).ConfigureAwait(false);
        }

        private Deserializer GetDeserializer()
        {
            return new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();
        }

        private TextReader GetReader(string source)
        {
            return new StringReader(source);
        }
    }
}
