namespace ProcessingTools.Processors.Processors.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors.Processors.Coordinates;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Contracts.Parsers;

    public class CoordinatesParser : ICoordinatesParser
    {
        private const string CurrentCoordinateWillNotBeProcessedErrorMessage = "Current coordinate will not be processed!";

        private readonly ICoordinateParser coordinateParser;
        private readonly ILogger logger;

        public CoordinatesParser(ICoordinateParser coordinateParser, ILogger logger)
        {
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
            this.logger = logger;
        }

        public Task<object> ParseAsync(XmlNode context) => Task.Run(() => this.ParseSync(context));

        public object ParseSync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (XmlNode coordinateNode in context.SelectNodes(XPathStrings.CoordinateWithEmptyLatitudeOrLongitude))
            {
                try
                {
                    this.ParseSingleCoordinateXmlNode(coordinateNode);
                }
                catch (Exception e)
                {
                    this.logger?.Log(type: LogType.Warning, exception: e, message: CurrentCoordinateWillNotBeProcessedErrorMessage);
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

        private void ParseSingleCoordinateXmlNode(XmlNode coordinateNode)
        {
            this.logger?.Log("\n{0}", coordinateNode.OuterXml);

            coordinateNode.InnerXml = Regex.Replace(coordinateNode.InnerXml, "(º|˚|<sup>o</sup>)", "°");

            string coordinateNodeInnerText = coordinateNode.InnerText;
            string coordinateType = coordinateNode.Attributes[AttributeNames.Type]?.InnerText;

            var coordinate = this.coordinateParser.ParseCoordinateString(coordinateNodeInnerText, coordinateType);

            this.SetCoordinatePartAttribute(coordinateNode, coordinate.Latitude, AttributeNames.Latitude);
            this.SetCoordinatePartAttribute(coordinateNode, coordinate.Longitude, AttributeNames.Longitude);
        }

        private void SetCoordinatePartAttribute(XmlNode coordinate, string coordinatePart, string attributeName)
        {
            if (!string.IsNullOrEmpty(coordinatePart))
            {
                coordinate.SafeSetAttributeValue(attributeName, coordinatePart);
            }
        }
    }
}
