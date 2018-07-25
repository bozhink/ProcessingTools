// <copyright file="CoordinatesParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Geo.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;

    /// <summary>
    /// Coordinates parser.
    /// </summary>
    public class CoordinatesParser : ICoordinatesParser
    {
        private const string CurrentCoordinateWillNotBeProcessedErrorMessage = "Current coordinate will not be processed!";

        private readonly ICoordinateParser coordinateParser;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesParser"/> class.
        /// </summary>
        /// <param name="coordinateParser">Single coordinate parser.</param>
        /// <param name="logger">Logger.</param>
        public CoordinatesParser(ICoordinateParser coordinateParser, ILogger<CoordinatesParser> logger)
        {
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context) => Task.Run(() => this.Parse(context));

        private object Parse(XmlNode context)
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
                    this.logger.LogError(e, CurrentCoordinateWillNotBeProcessedErrorMessage);
                }
            }

            this.CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows(context);

            return true;
        }

        private void CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows(XmlNode context)
        {
            foreach (XmlNode tableRowNode in context.SelectNodes(XPathStrings.TableRowWithCoordinatePartsWhichCanBeMerged))
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
            this.logger.LogDebug("\n{0}", coordinateNode.OuterXml);

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
