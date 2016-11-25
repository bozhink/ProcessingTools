namespace ProcessingTools.Processors.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Coordinates;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Geo;
    using ProcessingTools.Geo.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class CoordinatesParser : ICoordinatesParser
    {
        private const string CurrentCoordinateWillNotBeProcessedErrorMessage = "Current coordinate will not be processed!";

        private readonly ICoordinate2DParser coordinate2DParser;
        private readonly ILogger logger;

        public CoordinatesParser(ICoordinate2DParser coordinate2DParser, ILogger logger)
        {
            if (coordinate2DParser == null)
            {
                throw new ArgumentNullException(nameof(coordinate2DParser));
            }

            this.coordinate2DParser = coordinate2DParser;
            this.logger = logger;
        }

        public Task<object> Parse(XmlNode context) => Task.Run(() => this.ParseSync(context));

        public object ParseSync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (XmlNode coordinateNode in context.SelectNodes(XPathStrings.CoordinateWithEmptyLatitudeOrLongitude))
            {
                this.logger?.Log("\n{0}", coordinateNode.OuterXml);

                coordinateNode.InnerXml = Regex.Replace(coordinateNode.InnerXml, "(º|˚|<sup>o</sup>)", "°");

                try
                {
                    this.ParseSingleCoordinateXmlNode(coordinateNode);
                }
                catch (Exception e)
                {
                    this.logger?.Log(LogType.Warning, e, CurrentCoordinateWillNotBeProcessedErrorMessage);
                }
            }

            this.CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows(context);

            return true;
        }

        private void CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows(XmlNode context)
        {
            foreach (XmlNode tableRowNode in context.SelectNodes(XPathStrings.TableRowWithCoordinatePartsWichCanBeMerged))
            {
                XmlNode latitudeNode = tableRowNode.SelectSingleNode(XPathStrings.CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes);
                XmlNode longitudeNode = tableRowNode.SelectSingleNode(XPathStrings.CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes);

                latitudeNode.SafeSetAttributeValue(
                    AttributeNames.Longitude,
                    longitudeNode.Attributes[AttributeNames.Longitude].InnerText);

                longitudeNode.SafeSetAttributeValue(
                    AttributeNames.Latitude,
                    latitudeNode.Attributes[AttributeNames.Latitude].InnerText);
            }
        }

        // TODO: hard dependencies
        private void ParseSingleCoordinateXmlNode(XmlNode coordinateNode)
        {
            var latitude = new CoordinatePart(this.logger);
            var longitude = new CoordinatePart(this.logger);

            string coordinateNodeInnerText = coordinateNode.InnerText;
            string coordinateType = coordinateNode.Attributes[AttributeNames.Type]?.InnerText;

            this.coordinate2DParser.ParseCoordinateString(coordinateNodeInnerText, coordinateType, latitude, longitude);

            this.logger?.Log("{2} =\t{0};\t{3} =\t{1}\n", latitude.Value, longitude.Value, latitude.Type, longitude.Type);

            this.SetCoordinatePartAttribute(coordinateNode, latitude, AttributeNames.Latitude);
            this.SetCoordinatePartAttribute(coordinateNode, longitude, AttributeNames.Longitude);
        }

        private void SetCoordinatePartAttribute(XmlNode coordinate, ICoordinatePart coordinatePart, string attributeName)
        {
            if (coordinatePart.PartIsPresent)
            {
                coordinate.SafeSetAttributeValue(attributeName, coordinatePart.Value);
            }
        }
    }
}
