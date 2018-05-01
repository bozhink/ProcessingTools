﻿// <copyright file="YamlDotNetSerializationTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Serialization.Yaml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Services.Tests.Models;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// YamlDotNet serialization tests.
    /// </summary>
    [TestClass]
    public class YamlDotNetSerializationTests
    {
        /// <summary>
        /// De-serialization of simple list of objects should work.
        /// </summary>
        /// <remarks>
        /// See https://dotnetfiddle.net/UICBLd
        /// See https://stackoverflow.com/questions/25650113/how-to-parse-a-yaml-string
        /// </remarks>
        [TestMethod]
        public void Yaml_DeserializationOfSimpleListOfObjects_ShouldWork()
        {
            string yaml = @"
- Label: entry
  Layer: x
  Id: B35E246039E1CB70
- Ref: B35E246039E1CB70
  Label: Info
  Layer: x
  Id: CE0BEFC7022283A6
- Ref: CE0BEFC7022283A6
  Label: entry
  Layer: HttpWebRequest
  Id: 6DAA24FF5B777506
";

            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<List<Hashtable>>(new StringReader(yaml));

            foreach (var item in result)
            {
                Console.WriteLine("Item:");
                foreach (DictionaryEntry entry in item)
                {
                    Console.WriteLine("- {0} = {1}", entry.Key, entry.Value);
                }
            }
        }

        /// <summary>
        /// De-serialization of simple list of typed objects should work.
        /// </summary>
        [TestMethod]
        public void Yaml_DeserializationOfSimpleListOfTypedObjects_ShouldWork()
        {
            string yaml = @"
- Label: entry
  Layer: x
  Id: B35E246039E1CB70
- Ref: B35E246039E1CB70
  Label: Info
  Layer: x
  Id: CE0BEFC7022283A6
- Ref: CE0BEFC7022283A6
  Label: entry
  Layer: HttpWebRequest
  Id: 6DAA24FF5B777506
";

            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<IEnumerable<LayerModel>>(new StringReader(yaml));

            foreach (var item in result)
            {
                Console.WriteLine("Item:");
                Console.WriteLine($"- id = {item.Id}");
                Console.WriteLine($"- layer = {item.Layer}");
                Console.WriteLine($"- label = {item.Label}");
                Console.WriteLine($"- ref = {item.Ref}");
            }
        }

        /// <summary>
        /// De-serialization of simple list of typed objects with naming convention should work.
        /// </summary>
        [TestMethod]
        public void Yaml_DeserializationOfSimpleListOfTypedObjects_WithNamingConvention_ShouldWork()
        {
            string yaml = @"
- label: entry
  layer: x
  id: B35E246039E1CB70
- ref: B35E246039E1CB70
  label: Info
  layer: x
  id: CE0BEFC7022283A6
- ref: CE0BEFC7022283A6
  label: entry
  layer: HttpWebRequest
  id: 6DAA24FF5B777506
";

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var result = deserializer.Deserialize<IEnumerable<LayerModel>>(new StringReader(yaml));

            foreach (var item in result)
            {
                Console.WriteLine("Item:");
                Console.WriteLine($"- id = {item.Id}");
                Console.WriteLine($"- layer = {item.Layer}");
                Console.WriteLine($"- label = {item.Label}");
                Console.WriteLine($"- ref = {item.Ref}");
            }
        }

        /// <summary>
        /// De-serialization of <see cref="XmlReplaceRuleSetModel"/> should work.
        /// </summary>
        [TestMethod]
        public void Yaml_DeserializationOfXmlReplaceRuleSetModel_ShouldWork()
        {
            string yaml = @"
XPath: entry
Rules:
  - Pattern: x
    Replacement: X
  - Pattern: y
    Replacement: Y
";

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new PascalCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var result = deserializer.Deserialize<XmlReplaceRuleSetModel>(new StringReader(yaml));

            Assert.IsNotNull(result, "result");
            Assert.AreEqual("entry", result.XPath, "result.XPath");
            Assert.IsNotNull(result.Rules, "result.Rules");
            Assert.AreEqual(2, result.Rules.Count(), "result.Rules.Count");

            Console.WriteLine($"xpath: {result.XPath}");
            Console.WriteLine();
            foreach (var rule in result.Rules)
            {
                Console.WriteLine($"- pattern: {rule.Pattern}");
                Console.WriteLine($"- replacement: {rule.Replacement}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// De-serialization of list of <see cref="XmlReplaceRuleSetModel"/> should work.
        /// </summary>
        [TestMethod]
        public void Yaml_DeserializationOfListOfXmlReplaceRuleSetModel_ShouldWork()
        {
            string yaml = @"
- xPath: entry1
  rules:
    - pattern: x1
      replacement: X1
    - pattern: y1
      replacement: Y1
- xPath: entry2
  rules:
    - pattern: x2
      replacement: X2
    - pattern: y2
      replacement: Y2
";

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var result = deserializer.Deserialize<IEnumerable<XmlReplaceRuleSetModel>>(new StringReader(yaml));

            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count(), "result.Rules.Count");

            foreach (var item in result)
            {
                Console.WriteLine($"xpath: {item.XPath}");
                Console.WriteLine();
                foreach (var rule in item.Rules)
                {
                    Console.WriteLine($"- pattern: {rule.Pattern}");
                    Console.WriteLine($"- replacement: {rule.Replacement}");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// De-serialization of list of <see cref="XmlReplaceRuleSetModel"/> with complex strings should work.
        /// </summary>
        [TestMethod]
        public void Yaml_DeserializationOfListOfXmlReplaceRuleSetModel_WithComplexStrings_ShouldWork()
        {
            string yaml = @"
- xPath: //p[@id='1']
  rules:
    - pattern: >
        [A-Za-z]+(\W+){2,4}\1
      replacement: <z>$1</z>
    - pattern: y1
      replacement: Y1
- xPath: entry2
  rules:
    - pattern: x2
      replacement: X2
    - pattern: y2
      replacement: Y2
";

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            var result = deserializer.Deserialize<IEnumerable<XmlReplaceRuleSetModel>>(new StringReader(yaml));

            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count(), "result.Rules.Count");

            foreach (var item in result)
            {
                Console.WriteLine($"xpath: {item.XPath}");
                Console.WriteLine();
                foreach (var rule in item.Rules)
                {
                    Console.WriteLine($"- pattern: {rule.Pattern}");
                    Console.WriteLine($"- replacement: {rule.Replacement}");
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
