namespace ProcessingTools.Base.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class CoordinatesParser : Base, IParser
    {
        public CoordinatesParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public CoordinatesParser(IBase baseObject)
            : base(baseObject)
        {
        }

        internal enum CoordinateType
        {
            Latitude,
            Longitude,
            Null
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
                Alert.Log("\n" + coordinate.OuterXml + "\n");

                coordinate.InnerXml = Regex.Replace(coordinate.InnerXml, "(º|˚|<sup>o</sup>)", "°");

                string coordinateText = this.SimplifyCoordinateString(coordinate.InnerText);

                Alert.Log(">> " + coordinateText);

                CoordinatePart latitude = new CoordinatePart();
                CoordinatePart longitude = new CoordinatePart();

                bool hasLatitudePart = false;
                bool hasLongitudePart = false;
                if (coordinate.Attributes["type"] == null)
                {
                    string pattern = @"\A\W*?(\-?\d+[\.,\s]{1,3}\d+(?=\W*\s\W*\-?\d+[\.,\s]{1,3}\d+)|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SNWOE]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W+?((?<=\-?\d+[\.,\s]{1,3}\d+\W*\s\W*?)\-?\d+[\.,\s]{1,3}\d+|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W*?\Z";
                    string leftPart = Regex.Replace(coordinateText, pattern, "$1");
                    string rightPart = Regex.Replace(coordinateText, pattern, "$16");
                    string latitudeString, longitudeString;

                    if ((leftPart.Contains("N") || leftPart.Contains("S")) && (rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O")))
                    {
                        if (leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O") || rightPart.Contains("N") || rightPart.Contains("S"))
                        {
                            Alert.Log("Can not parse coordinate.");
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
                            Alert.Log("Can not parse coordinate.");
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

                    Alert.Log("Latitude =\t" + latitudeString + ";\tLongitude =\t" + longitudeString);

                    Match latMatch = Regex.Match(latitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    hasLatitudePart = latMatch.Success;
                    Match lngMatch = Regex.Match(longitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    hasLongitudePart = lngMatch.Success;

                    if (hasLatitudePart || hasLongitudePart)
                    {
                        if (latMatch.NextMatch().Success)
                        {
                            Alert.Log("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else if (lngMatch.NextMatch().Success)
                        {
                            Alert.Log("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude.ParseString(hasLatitudePart ? latMatch.Value : string.Empty);
                            longitude.ParseString(hasLongitudePart ? lngMatch.Value : string.Empty);

                            Alert.Log("Latitude =\t" + latitude.CoordinateString + ";\tLongitude =\t" + longitude.CoordinateString);
                            Alert.Log(latitude.Type + " =\t" + latitude.CoordinateValue + ";\t" +
                                longitude.Type + " =\t" + longitude.CoordinateValue);
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
                            Alert.Log("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude.ParseString(latMatch.Value);
                            Alert.Log(latitude.Type + " =\t" + latitude.CoordinateString);
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
                            Alert.Log("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            longitude.ParseString(lngMatch.Value);
                            Alert.Log(longitude.Type + " =\t" + longitude.CoordinateString);
                        }
                    }
                }

                Console.WriteLine("{2} =\t{0};\t{3} =\t{1}\n", latitude.CoordinateValue, longitude.CoordinateValue, latitude.Type, longitude.Type);

                try
                {
                    if (hasLatitudePart && coordinate.Attributes["latitude"].InnerText == string.Empty)
                    {
                        coordinate.Attributes["latitude"].InnerText = latitude.CoordinateValue;
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (hasLongitudePart && coordinate.Attributes["longitude"].InnerText == string.Empty)
                    {
                        coordinate.Attributes["longitude"].InnerText = longitude.CoordinateValue;
                    }
                }
                catch (Exception)
                {
                }
            }

            foreach (XmlNode node in this.XmlDocument.SelectNodes("//tr[count(.//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)=''])=1][count(.//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!=''])=1]", this.NamespaceManager))
            {
                XmlNode latCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']", this.NamespaceManager);
                XmlNode lngCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']", this.NamespaceManager);

                latCoordinate.Attributes["longitude"].InnerText = lngCoordinate.Attributes["longitude"].InnerText;
                lngCoordinate.Attributes["latitude"].InnerText = latCoordinate.Attributes["latitude"].InnerText;
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

        private class CoordinatePart
        {
            private int decimalCoordinateSign;
            private double decimalCoordinateValue;
            private string coordinateString;
            private CoordinateType type;

            public CoordinatePart()
            {
                this.decimalCoordinateSign = 1;
                this.decimalCoordinateValue = 0.0;
                this.coordinateString = string.Empty;
                this.type = CoordinateType.Null;
            }

            public string CoordinateString
            {
                get
                {
                    return this.coordinateString;
                }
            }

            public string Type
            {
                get
                {
                    return this.type.ToString();
                }
            }

            public string CoordinateValue
            {
                get
                {
                    return this.decimalCoordinateValue.ToString("f5");
                }
            }

            public void ParseString(string coordinatePartString)
            {
                this.coordinateString = Regex.Replace(coordinatePartString, @"\s+", " ");
                this.coordinateString = Regex.Replace(this.coordinateString, @"\s+\-\s*|\s*\-\s+", "-");
                if (Regex.Match(this.coordinateString, @"\-.*?[NSWEO]|[NSWEO].*?\-").Success)
                {
                    this.coordinateString = Regex.Replace(this.coordinateString, @"\-", string.Empty);
                }

                bool hasN = this.coordinateString.Contains("N");
                bool hasS = this.coordinateString.Contains("S");
                bool hasE = this.coordinateString.Contains("E");
                bool hasW = this.coordinateString.Contains("W");
                bool hasO = this.coordinateString.Contains("O");
                if ((hasN || hasS) && !(hasE || hasW || hasO))
                {
                    this.type = CoordinateType.Latitude;
                }
                else if (!(hasN || hasS) && (hasE || hasW || hasO))
                {
                    this.type = CoordinateType.Longitude;
                }
                else
                {
                    this.type = CoordinateType.Null;
                }

                //// There is a linguistic problem: O = Ost (German) = East, and O = Oeste (Spanish) = West
                //// Here is supposed that O = Ost
                this.decimalCoordinateSign = hasS || hasW ? -1 : 1;

                this.coordinateString = Regex.Replace(this.coordinateString, @"\s*[NSWEO]\s*", string.Empty);
                this.coordinateString = Regex.Replace(Regex.Replace(this.coordinateString, @"(\d+),(\d+)", "$1.$2"), @"[^\d\.\-]+", " ");
                this.coordinateString = Regex.Replace(this.coordinateString, @"(?<=\d)\s+(?=\.)|(?<=\.)\s+(?=\d)", string.Empty);
                this.coordinateString = Regex.Replace(this.coordinateString, @"\A(.*?\d+)\.(\d+\.\d+.*)\Z", "$1 $2");
                this.decimalCoordinateValue = this.decimalCoordinateSign * this.ParseOneCoordinate(this.coordinateString);
            }

            private double ParseOneCoordinate(string str)
            {
                double deg, mm, ss;
                string degreesString, minutesString, secondsString;
                if (Regex.Match(str, @"^.*?(\-?\d+(\.\d+)?).*$").Success)
                {
                    degreesString = Regex.Replace(str, @"^.*?(\-?\d+(\.\d+)?).*$", "$1");
                }
                else
                {
                    degreesString = " ";
                }

                if (Regex.Match(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$").Success)
                {
                    minutesString = Regex.Replace(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$2");
                }
                else
                {
                    minutesString = " ";
                }

                if (Regex.Match(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$").Success)
                {
                    secondsString = Regex.Replace(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$3");
                }
                else
                {
                    secondsString = " ";
                }

                Alert.Log("DEG: " + Regex.Replace(degreesString, "\\s+", "#") + " MM: " + Regex.Replace(minutesString, "\\s+", "#") + " SS: " + Regex.Replace(secondsString, "\\s+", "#"));

                try
                {
                    deg = degreesString.Contains(" ") ? 0.0 : double.Parse(degreesString);
                    mm = minutesString.Contains(" ") ? 0.0 : double.Parse(minutesString);
                    ss = secondsString.Contains(" ") ? 0.0 : double.Parse(secondsString);

                    return deg + ((mm + (ss / 60.0)) / 60.0);
                }
                catch (System.ArgumentNullException)
                {
                    Alert.Log("ArgumentNullException in Coordinate parameter = " + str);
                    return 0.0;
                }
                catch (System.FormatException)
                {
                    Alert.Log("FormatException in Coordinate parameter = " + str);
                    return 0.0;
                }
                catch (System.OverflowException)
                {
                    Alert.Log("OverflowException in Coordinate parameter = " + str);
                    return 0.0;
                }
            }
        }
    }
}
