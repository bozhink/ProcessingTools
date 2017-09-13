namespace ProcessingTools.Geo.Tests
{
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models.Json.GeoJson;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [TestClass]
    public class GeoJsonTests
    {
        private const string JsonGeometryPointStringSample = @"{
                ""type"": ""Feature"",
                ""geometry"": { ""type"": ""Point"", ""coordinates"": [ 125.6, 10.1 ] },
                ""properties"": { ""name"": ""Dinagat Islands"" }
            }";

        [TestMethod]
        public void GeoJson_DeserializationOfSampleGeometryPoint_ShouldWork()
        {
            var point = JsonConvert.DeserializeObject<GeoJsonFeature>(JsonGeometryPointStringSample);

            Assert.AreEqual(
                GeoJsonType.Feature.ToString(),
                point.Type,
                "point.Type should be 'Feature'.");

            Assert.AreEqual(
                GeoJsonType.Point.ToString(),
                point.Geometry.Type,
                "point.Geometry.Type schold be 'Point'.");

            double?[] coordinates = point
                .Geometry
                .Coordinates
                .Select(c => c as double?)
                .ToArray();

            Assert.AreEqual(2, coordinates.Length, "The number of coordinate points should be equal to 2.");

            Assert.AreEqual(125.6, coordinates[0], "coordinates[0] should be equal to 125.6.");

            Assert.AreEqual(10.1, coordinates[1], "coordinates[1] should be equal to 10.1.");
        }

        [TestMethod]
        public void GeoJson_SerializationOfSampleGeometryPoint_ShouldWork()
        {
            var point = new GeoJsonFeature
            {
                Type = GeoJsonType.Feature.ToString(),
                Geometry = new GeoJsonGeometry
                {
                    Type = GeoJsonType.Point.ToString(),
                    Coordinates = new[] { 125.6, 10.1 }.Select(c => c as object).ToList()
                },
                Properties = new
                {
                    name = "Dinagat Islands"
                }
            };

            var thisJToken = JObject.Parse(JsonConvert.SerializeObject(point));
            var smpleJToken = JObject.Parse(JsonGeometryPointStringSample);

            Assert.IsTrue(JToken.DeepEquals(thisJToken, smpleJToken));
        }

        [TestMethod]
        public void GeoJson_DeserializationOfCartoDbSample_ShouldWork()
        {
            string cartoDbSampleFileName = ConfigurationManager.AppSettings["CartoDbSampleGeoJson"];

            string cartoDbSampleJsonText = File.ReadAllText(cartoDbSampleFileName);

            var features = JsonConvert.DeserializeObject<GeoJsonFeatureCollection>(cartoDbSampleJsonText);

            Assert.IsNotNull(features, "Deserialized object should not be null.");

            Assert.AreEqual("FeatureCollection", features.Type, "Deserialized object should be of type FeatureCollection.");

            Assert.AreEqual(1121, features.Features.Count(), "Number of features should match.");

            foreach (var feature in features.Features)
            {
                Assert.AreEqual("Feature", feature.Type, "Feature type should be Feature.");

                Assert.IsNotNull(feature.Geometry, "Geometry should not be null.");

                Assert.AreEqual("Point", feature.Geometry.Type, "Geometry type should be Point.");

                Assert.IsNotNull(feature.Geometry.Coordinates, "Coordinates should not be null.");

                Assert.IsNotNull(feature.Properties, "Properties should not be null.");
            }
        }
    }
}
