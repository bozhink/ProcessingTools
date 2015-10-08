namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class CoordinatesParser : Base, IBaseParser
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
            //// S21°59'01, W64°12'30 is valid
            //// 8.77522 N, -70.80349 E
            //// -3.08732°N, -79.71493°W -->> (\-?\d+\.\d+°\w,\s*\-?\d+\.\d+°\w)      (\-?\d+\.\d+\s*°\s*\w,\s*\-?\d+\.\d+\s*°\s*\w)

            ////03°14.78S, 72°54.61W
            ////03°15’S 72°54’W
            ////20°20.1N 74°33.6W

            ////37°08'09.4"N, 8°23'04.2"W
            ////08º48’23’’S, 115º56’24’’E
            ////20°20.1N 74°33.6W

            foreach (XmlNode coordinate in this.XmlDocument.SelectNodes("//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']", this.NamespaceManager))
            {
                this.logger?.Log("\n{0}\n", coordinate.OuterXml);

                coordinate.InnerXml = Regex.Replace(coordinate.InnerXml, "(º|˚|<sup>o</sup>)", "°");

                string coordinateText = this.SimplifyCoordinateString(coordinate.InnerText);

                this.logger?.Log(">> {0}", coordinateText);

                CoordinatePart latitude = new CoordinatePart(this.logger);
                CoordinatePart longitude = new CoordinatePart(this.logger);

                bool hasLatitudePart = false;
                bool hasLongitudePart = false;
                if (coordinate.Attributes["type"] == null)
                {
                    const string Pattern = @"\A\W*?(\-?\d+[\.,\s]{1,3}\d+(?=\W*\s\W*\-?\d+[\.,\s]{1,3}\d+)|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SNWOE]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W+?((?<=\-?\d+[\.,\s]{1,3}\d+\W*\s\W*?)\-?\d+[\.,\s]{1,3}\d+|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W*?\Z";

                    string leftPart = Regex.Replace(coordinateText, Pattern, "$1");
                    string rightPart = Regex.Replace(coordinateText, Pattern, "$16");
                    string latitudeString, longitudeString;

                    if ((leftPart.Contains("N") || leftPart.Contains("S")) && (rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O")))
                    {
                        if (leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O") || rightPart.Contains("N") || rightPart.Contains("S"))
                        {
                            this.logger?.Log("Can not parse coordinate.");
                            continue;
                        }
                        else
                        {
                            latitudeString = leftPart;
                            longitudeString = rightPart;
                        }
                    }
                    else if ((leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O")) && (rightPart.Contains("N") || rightPart.Contains("S")))
                    {
                        if (leftPart.Contains("N") || leftPart.Contains("S") || rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O"))
                        {
                            this.logger?.Log("Can not parse coordinate.");
                            continue;
                        }
                        else
                        {
                            latitudeString = rightPart;
                            longitudeString = leftPart;
                        }
                    }
                    else
                    {
                        latitudeString = leftPart;
                        longitudeString = rightPart;
                    }

                    this.logger?.Log("Latitude =\t{0};\tLongitude =\t{1}", latitudeString, longitudeString);

                    Match latMatch = Regex.Match(latitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");

                    hasLatitudePart = latMatch.Success;

                    Match lngMatch = Regex.Match(longitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");

                    hasLongitudePart = lngMatch.Success;

                    if (hasLatitudePart || hasLongitudePart)
                    {
                        if (latMatch.NextMatch().Success)
                        {
                            this.logger?.Log("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else if (lngMatch.NextMatch().Success)
                        {
                            this.logger?.Log("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude.ParseString(hasLatitudePart ? latMatch.Value : string.Empty);
                            longitude.ParseString(hasLongitudePart ? lngMatch.Value : string.Empty);

                            this.logger?.Log(
                                "Latitude =\t{0};\tLongitude =\t{1}\n{2} =\t{3};\t{4} =\t{5}",
                                latitude.CoordinateString,
                                longitude.CoordinateString,
                                latitude.Type,
                                latitude.CoordinateValue,
                                longitude.Type,
                                longitude.CoordinateValue);
                        }
                    }
                }
                else if (coordinate.Attributes["type"].InnerText == "latitude")
                {
                    Match latMatch = Regex.Match(coordinateText, @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");

                    hasLatitudePart = latMatch.Success;

                    if (hasLatitudePart)
                    {
                        if (latMatch.NextMatch().Success)
                        {
                            this.logger?.Log("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude.ParseString(latMatch.Value);
                            this.logger?.Log("{0} =\t{1}", latitude.Type, latitude.CoordinateString);
                        }
                    }
                }
                else if (coordinate.Attributes["type"].InnerText == "longitude")
                {
                    Match lngMatch = Regex.Match(coordinateText, @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");

                    hasLongitudePart = lngMatch.Success;

                    if (hasLongitudePart)
                    {
                        if (lngMatch.NextMatch().Success)
                        {
                            this.logger?.Log("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            longitude.ParseString(lngMatch.Value);
                            this.logger?.Log("{0} =\t{1}", longitude.Type, longitude.CoordinateString);
                        }
                    }
                }

                this.logger?.Log(
                    "{2} =\t{0};\t{3} =\t{1}\n",
                    latitude.CoordinateValue,
                    longitude.CoordinateValue,
                    latitude.Type,
                    longitude.Type);

                this.SetLatitudeAttribute(coordinate, latitude, hasLatitudePart);

                this.SetLongitudeAttribute(coordinate, longitude, hasLongitudePart);
            }

            foreach (XmlNode node in this.XmlDocument.SelectNodes("//tr[count(.//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)=''])=1][count(.//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!=''])=1]", this.NamespaceManager))
            {
                XmlNode latCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']", this.NamespaceManager);
                XmlNode lngCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']", this.NamespaceManager);

                if (latCoordinate.Attributes["longitude"] == null)
                {
                    XmlAttribute longitudeAttribute = latCoordinate.OwnerDocument.CreateAttribute("longitude");
                    latCoordinate.Attributes.Append(longitudeAttribute);
                }

                if (lngCoordinate.Attributes["latitude"] == null)
                {
                    XmlAttribute latitudeAttribute = latCoordinate.OwnerDocument.CreateAttribute("latitude");
                    lngCoordinate.Attributes.Append(latitudeAttribute);
                }

                latCoordinate.Attributes["longitude"].InnerText = lngCoordinate.Attributes["longitude"].InnerText;
                lngCoordinate.Attributes["latitude"].InnerText = latCoordinate.Attributes["latitude"].InnerText;
            }
        }

        private void SetLongitudeAttribute(XmlNode coordinate, CoordinatePart longitude, bool hasLongitudePart)
        {
            try
            {
                if (hasLongitudePart)
                {
                    if (coordinate.Attributes["longitude"] == null)
                    {
                        XmlAttribute longitudeAttribute = this.XmlDocument.CreateAttribute("longitude");
                        coordinate.Attributes.Append(longitudeAttribute);
                    }

                    if (coordinate.Attributes["longitude"].InnerText == string.Empty)
                    {
                        coordinate.Attributes["longitude"].InnerText = longitude.CoordinateValue;
                    }
                }
            }
            catch
            {
            }
        }

        private void SetLatitudeAttribute(XmlNode coordinate, CoordinatePart latitude, bool hasLatitudePart)
        {
            try
            {
                if (hasLatitudePart)
                {
                    if (coordinate.Attributes["latitude"] == null)
                    {
                        XmlAttribute latitudeAttribute = this.XmlDocument.CreateAttribute("latitude");
                        coordinate.Attributes.Append(latitudeAttribute);
                    }

                    if (coordinate.Attributes["latitude"].InnerText == string.Empty)
                    {
                        coordinate.Attributes["latitude"].InnerText = latitude.CoordinateValue;
                    }
                }
            }
            catch
            {
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

            return coordinateText;
        }
    }
}