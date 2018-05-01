// <copyright file="YamlDotNetSerializationTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Serialization.Yaml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Services.Tests.Models;
    using YamlDotNet.Serialization;

    /// <summary>
    /// YamlDotNet serialization tests.
    /// </summary>
    [TestClass]
    public class YamlDotNetSerializationTests
    {
        /// <summary>
        /// Serialization of simple list of objects should work.
        /// </summary>
        /// <remarks>
        /// See https://dotnetfiddle.net/UICBLd
        /// See https://stackoverflow.com/questions/25650113/how-to-parse-a-yaml-string
        /// </remarks>
        [TestMethod]
        public void Yaml_SerializationOfSimpleListOfObjects_ShouldWork()
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
        /// Serialization of simple list of typed objects should work.
        /// </summary>
        [TestMethod]
        public void Yaml_SerializationOfSimpleListOfTypedObjects_ShouldWork()
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
            var result = deserializer.Deserialize<List<LayerModel>>(new StringReader(yaml));

            foreach (var item in result)
            {
                Console.WriteLine("Item:");
                Console.WriteLine($"- id = {item.Id}");
                Console.WriteLine($"- layer = {item.Layer}");
                Console.WriteLine($"- label = {item.Label}");
                Console.WriteLine($"- ref = {item.Ref}");
            }
        }
    }
}
