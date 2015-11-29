namespace ProcessingTools.Geo.Tests
{
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
        public void GeoJson_DeserializationOfSampleGeometryPoint_SchouldWork()
        {
            var point = JsonConvert.DeserializeObject<GeoJsonFeature>(JsonGeometryPointStringSample);

            Assert.AreEqual(
                GeoJsonType.Feature.ToString(),
                point.Type,
                "point.Type schould be 'Feature'.");

            Assert.AreEqual(
                GeoJsonType.Point.ToString(),
                point.Geometry.Type,
                "point.Geometry.Type schold be 'Point'.");

            double?[] coordinates = point
                .Geometry
                .Coordinates
                .Select(c => c as double?)
                .ToArray();

            Assert.AreEqual(2, coordinates.Length, "The number of coordinate points schould be equal to 2.");

            Assert.AreEqual(125.6, coordinates[0], "coordinates[0] schould be equal to 125.6.");

            Assert.AreEqual(10.1, coordinates[1], "coordinates[1] schould be equal to 10.1.");
        }

        [TestMethod]
        public void GeoJson_SerializationOfSampleGeometryPoint_SchouldWork()
        {
            var point = new GeoJsonFeature
            {
                Type = GeoJsonType.Feature.ToString(),
                Geometry = new GeoJsonGeometry
                {
                    Type = GeoJsonType.Point.ToString(),
                    Coordinates = (new double[] { 125.6, 10.1 }).Select(c => c as object).ToList()
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
    }
}
