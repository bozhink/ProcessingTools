namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;
    using ProcessingTools.Extensions;
    using ProcessingTools.Geo;

    public class CoordinatesParser : Base, IParser
    {
        private ILogger logger;

        public CoordinatesParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public CoordinatesParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Parse()
        {
            foreach (XmlNode coordinateNode in this.XmlDocument.SelectNodes("//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']", this.NamespaceManager))
            {
                this.logger?.Log("\n{0}", coordinateNode.OuterXml);

                coordinateNode.InnerXml = Regex.Replace(coordinateNode.InnerXml, "(º|˚|<sup>o</sup>)", "°");

                try
                {
                    this.ParseSingleCoordinateXmlNode(coordinateNode);
                }
                catch (ApplicationException)
                {
                    try
                    {
                        string coordinateText = this.SimplifyCoordinateString(coordinateNode.InnerText);

                        var latitudeString = Regex.Replace(coordinateText, @"\A.*([NS])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");
                        var longitudeString = Regex.Replace(coordinateText, @"\A.*([EW])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");

                        Console.WriteLine(latitudeString);
                        Console.WriteLine(longitudeString);

                        CoordinatePart latitude = new CoordinatePart(this.logger)
                        {
                            PartIsPresent = false
                        };

                        CoordinatePart longitude = new CoordinatePart(this.logger)
                        {
                            PartIsPresent = false
                        };

                        Coordinate coordinate = new Coordinate
                        {
                            Latitude = latitudeString,
                            Longitude = longitudeString
                        };

                        this.ParseCoordinateObject(latitude, longitude, coordinate);
                        this.logger?.Log("{2} =\t{0};\t{3} =\t{1}\n", latitude.Value, longitude.Value, latitude.Type, longitude.Type);

                        this.SetCoordinatePartAttribute(coordinateNode, latitude, "latitude");
                        this.SetCoordinatePartAttribute(coordinateNode, longitude, "longitude");
                    }
                    catch (ApplicationException e)
                    {
                        this.logger?.Log(LogType.Warning, e, "Current coordinate will not be processed!");
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(LogType.Warning, e, "Current coordinate will not be processed!");
                }
            }

            this.CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows();
        }

        private void CombineCoordinatePartsOfColumnSeparatedCoordinatesInTableRows()
        {
            foreach (XmlNode tableRowNode in this.XmlDocument.SelectNodes("//tr[count(.//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)=''])=1][count(.//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!=''])=1]", this.NamespaceManager))
            {
                XmlNode latitudeNode = tableRowNode.SelectSingleNode(".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']", this.NamespaceManager);

                XmlNode longitudeNode = tableRowNode.SelectSingleNode(".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']", this.NamespaceManager);

                latitudeNode.SafeSetAttributeValue("longitude", longitudeNode.Attributes["longitude"].InnerText);
                longitudeNode.SafeSetAttributeValue("latitude", latitudeNode.Attributes["latitude"].InnerText);
            }
        }

        private void ParseSingleCoordinateXmlNode(XmlNode coordinateNode)
        {
            //// S21°59'01, W64°12'30 is valid
            //// 8.77522 N, -70.80349 E
            //// -3.08732°N, -79.71493°W -->> (\-?\d+\.\d+°\w,\s*\-?\d+\.\d+°\w)      (\-?\d+\.\d+\s*°\s*\w,\s*\-?\d+\.\d+\s*°\s*\w)

            ////03°14.78S, 72°54.61W
            ////03°15’S 72°54’W
            ////20°20.1N 74°33.6W

            ////37°08'09.4"N, 8°23'04.2"W
            ////08º48’23’’S, 115º56’24’’E
            ////20°20.1N 74°33.6W

            string coordinateText = this.SimplifyCoordinateString(coordinateNode.InnerText);

            //// this.logger?.Log(">> {0}", coordinateText);

            CoordinatePart latitude = new CoordinatePart(this.logger)
            {
                PartIsPresent = false
            };

            CoordinatePart longitude = new CoordinatePart(this.logger)
            {
                PartIsPresent = false
            };

            if (coordinateNode.Attributes["type"] == null)
            {
                Coordinate coordinate = new Coordinate();

                {
                    const string RepeatedDirectionsErrorMessage = "Repeated directions in the coordinate string.";

                    // TODO: Error on 34.47325°, 132.10362°
                    const string CoordinateParsePattern = @"\A\W*?(\-?\d+[\.,\s]{1,3}\d+(?=\W*\s\W*\-?\d+[\.,\s]{1,3}\d+)|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SNWOE]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W+?((?<=\-?\d+[\.,\s]{1,3}\d+\W*\s\W*?)\-?\d+[\.,\s]{1,3}\d+|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W*?\Z";

                    string leftPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$1");
                    string rightPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$16");

                    if ((leftPart.Contains("N") || leftPart.Contains("S")) &&
                        (rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O")))
                    {
                        if (leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O") ||
                            rightPart.Contains("N") || rightPart.Contains("S"))
                        {
                            throw new ApplicationException(RepeatedDirectionsErrorMessage);
                        }
                        else
                        {
                            coordinate.Latitude = leftPart;
                            coordinate.Longitude = rightPart;
                        }
                    }
                    else if ((leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O")) && (rightPart.Contains("N") || rightPart.Contains("S")))
                    {
                        if (leftPart.Contains("N") || leftPart.Contains("S") || rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O"))
                        {
                            throw new ApplicationException(RepeatedDirectionsErrorMessage);
                        }
                        else
                        {
                            coordinate.Latitude = rightPart;
                            coordinate.Longitude = leftPart;
                        }
                    }
                    else
                    {
                        coordinate.Latitude = leftPart;
                        coordinate.Longitude = rightPart;
                    }

                    //// this.logger?.Log("Latitude =\t{0};\tLongitude =\t{1}", coordinate.Latitude, coordinate.Longitude);
                }

                this.ParseCoordinateObject(latitude, longitude, coordinate);
            }
            else if (coordinateNode.Attributes["type"].InnerText == "latitude")
            {
                this.ParseLatitudeTypeCoordinate(coordinateText, latitude);
            }
            else if (coordinateNode.Attributes["type"].InnerText == "longitude")
            {
                this.ParseLongitudeTypeCoordinate(coordinateText, longitude);
            }

            this.logger?.Log("{2} =\t{0};\t{3} =\t{1}\n", latitude.Value, longitude.Value, latitude.Type, longitude.Type);

            this.SetCoordinatePartAttribute(coordinateNode, latitude, "latitude");
            this.SetCoordinatePartAttribute(coordinateNode, longitude, "longitude");
        }

        private void ParseCoordinateObject(CoordinatePart latitude, CoordinatePart longitude, Coordinate coordinate)
        {
            const string LatitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

            this.ParseSinglePartTypeCoordinate(coordinate.Latitude, latitude, LatitudeMatchPattern);

            const string LongitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

            this.ParseSinglePartTypeCoordinate(coordinate.Longitude, longitude, LongitudeMatchPattern);
        }

        private void ParseLongitudeTypeCoordinate(string coordinateText, CoordinatePart longitude)
        {
            const string MatchLongitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

            this.ParseSinglePartTypeCoordinate(coordinateText, longitude, MatchLongitudePartPattern);
        }

        private void ParseLatitudeTypeCoordinate(string coordinateText, CoordinatePart latitude)
        {
            const string MatchLatitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

            this.ParseSinglePartTypeCoordinate(coordinateText, latitude, MatchLatitudePartPattern);
        }

        private void ParseSinglePartTypeCoordinate(string coordinateText, CoordinatePart coordinatePart, string matchPartPattern)
        {
            Match matchPart = Regex.Match(coordinateText, matchPartPattern);

            coordinatePart.PartIsPresent = matchPart.Success;

            if (coordinatePart.PartIsPresent)
            {
                if (matchPart.NextMatch().Success)
                {
                    throw new ApplicationException($"Multiple matches of {coordinatePart.Type}");
                }
                else
                {
                    coordinatePart.CoordinatePartString = coordinatePart.PartIsPresent ? matchPart.Value : string.Empty;
                    coordinatePart.Parse();
                    //// this.logger?.Log("{0} =\t{1}", coordinatePart.Type, coordinatePart.CoordinatePartString);
                }
            }
        }

        private void SetCoordinatePartAttribute(XmlNode coordinate, CoordinatePart coordinatePart, string attributeName)
        {
            if (coordinatePart.PartIsPresent)
            {
                coordinate.SafeSetAttributeValue(attributeName, coordinatePart.Value);
            }
        }

        private string SimplifyCoordinateString(string coordinateString)
        {
            string coordinateText = Regex.Replace(coordinateString, "[–—−-]", "-");

            coordinateText = Regex.Replace(coordinateText, @"[^EWONS\d\W]+", " "); // Remove text
            coordinateText = Regex.Replace(coordinateText, @"\s[a-z]+\s", " ");
            coordinateText = Regex.Replace(coordinateText, @"\-\s+(?=\d)", "-");

            //// 29.63527EN, 82.37111EW
            coordinateText = Regex.Replace(coordinateText, "E(?=[EWONS])", " ");

            // Remove some unused special characters
            coordinateText = Regex.Replace(coordinateText, @"[\\\/\|<>\!\?\*:;]", " ");
            coordinateText = Regex.Replace(coordinateText, @"\s{2,}", " ");

            //// N33.50.13, E107.48.52 --> N33 50 13, E107 48 52
            coordinateText = Regex.Replace(coordinateText, @"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9])\s*\.\s*([0-5][0-9](\s*\.\s*\d+)?(?!\.)(?!\d))", "$1 $2 $3");

            //// N33.50.613, E107.48.524 --> N33 50.613, E107 48.524
            coordinateText = Regex.Replace(coordinateText, @"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9]\s*\.\s*[0-9]{3,})", "$1 $2");

            //// S39°34 283, W71°29 908
            coordinateText = Regex.Replace(coordinateText, @"(?<=°\s*\d\d)\s+(?=\d\d\d)", ".");

            //// S39°34'283"W 71°29'908"
            coordinateText = Regex.Replace(coordinateText, @"(?<=°\s*\d\d)\s*'\s*(\d\d\d)\s*""", ".$1 ");

            //// 20. 58139°S, 164.76444°E
            coordinateText = Regex.Replace(coordinateString, @"(?<=\d)(\s*[,\.]\s+|\s+[,\.]\s*)(?=\d)", ".");

            //// 22.14158°’S, 166.67993 °E
            coordinateText = Regex.Replace(coordinateString, @"\W*°\W+|W+°\W*", "°");

            return coordinateText;
        }
    }
}