using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
    public class Coordinates : Base
    {
        public Coordinates()
            : base()
        {
        }

        public Coordinates(string xml)
            : base(xml)
        {
        }

        public void TagCoordinates()
        {
            // Format deg symbol
            xml = Regex.Replace(xml, "(\\d)([º°˚]|<sup>o</sup>)", "$1°");

            // Tag coordinates
            string replace = "<locality-coordinates latitude=\"\" longitude=\"\">$1</locality-coordinates>";

            xml = Regex.Replace(xml, @"(([NSEW])((\d+\.)?\d+°\d+(\.\d+)?\'(\d+(\.\d+)?\&quot;)?)\s*?\W?\s*([NSEW])((\d+\.)?\d+°\d+(\.\d+)?\'(\d+(\.\d+)?\&quot;)?))", replace);
            xml = Regex.Replace(xml, @"(([NSEW])((\d+\.)?\d+°)\s*?\W?\s*([NSEW])((\d+\.)?\d+°))", replace);

            xml = Regex.Replace(xml, @"((\d+\.\d+)\s*([NSEW])\s*\,?\s*(\d+\.\d+)\s*([NSEW]))", replace);
            xml = Regex.Replace(xml, @"(((\d+\.)?\d+°[^<>]{0,20}?[SWNE])\s*?\W?\s*((\d+\.)?\d+°[^</>]{0,20}?[SWNE]))", replace);
            xml = Regex.Replace(xml, @"((\-?\d{1,3}\.\d{3,6})\s*(;|,)\s*(\-?\d{1,3}\.\d{3,6}))", replace);
        }

        public void ParseCoordinates()
        {
            // S21°59'01, W64°12'30 is valid
            // 8.77522 N, -70.80349 E
            // -3.08732°N, -79.71493°W -->> (\-?\d+\.\d+°\w,\s*\-?\d+\.\d+°\w)      (\-?\d+\.\d+\s*°\s*\w,\s*\-?\d+\.\d+\s*°\s*\w)

            //03°14.78S, 72°54.61W
            //03°15’S 72°54’W
            //20°20.1N 74°33.6W

            //37°08'09.4"N, 8°23'04.2"W
            //08º48’23’’S, 115º56’24’’E
            //20°20.1N 74°33.6W

            ParseXmlStringToXmlDocument();
            foreach (XmlNode coordinate in xmlDocument.SelectNodes("//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']", namespaceManager))
            {
                coordinate.InnerXml = Regex.Replace(coordinate.InnerXml, "(º|˚|<sup>o</sup>)", "°");

                string coordinateText = Regex.Replace(coordinate.InnerText, "[–—−-]", "-");

                //Alert.Message("> " + coordinateText);
                coordinateText = Regex.Replace(coordinateText, @"[^EWONS\d\W]+", " "); // Remove text
                //Alert.Message("> " + coordinateText);

                coordinateText = Regex.Replace(coordinateText, @"\s[a-z]+\s", " ");
                coordinateText = Regex.Replace(coordinateText, @"\-\s+(?=\d)", "-");
                // 29.63527EN, 82.37111EW
                coordinateText = Regex.Replace(coordinateText, "E(?=[EWONS])", " ");

                // Remove some unused species characters
                coordinateText = Regex.Replace(coordinateText, @"[\\\/\|<>\!\?\*:;]", " ");
                coordinateText = Regex.Replace(coordinateText, @"\s{2,}", " ");

                // N33.50.13, E107.48.52 --> N33 50 13, E107 48 52
                coordinateText = Regex.Replace(coordinateText, @"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9])\s*\.\s*([0-5][0-9](\s*\.\s*\d+)?(?!\.)(?!\d))", "$1 $2 $3");
                // N33.50.613, E107.48.524 --> N33 50.613, E107 48.524
                coordinateText = Regex.Replace(coordinateText, @"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9]\s*\.\s*[0-9]{3,})", "$1 $2");

                Alert.Message("\n>> " + coordinateText);

                Match latMatch = null;
                Match lngMatch = null;

                CoordinatePart latitude = new CoordinatePart();
                CoordinatePart longitude = new CoordinatePart();

                Alert.Message();
                Alert.Message(coordinate.OuterXml);

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
                            Alert.Message("Can not parse coordinate.");
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
                            Alert.Message("Can not parse coordinate.");
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
                    Alert.Message("Latitude =\t" + latitudeString + ";\tLongitude =\t" + longitudeString);

                    latMatch = Regex.Match(latitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    lngMatch = Regex.Match(longitudeString, @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    if (latMatch.Success || lngMatch.Success)
                    {
                        if (latMatch.NextMatch().Success)
                        {
                            Alert.Message("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else if (lngMatch.NextMatch().Success)
                        {
                            Alert.Message("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude = ParseCoordinatePartString(latMatch.Success ? latMatch.Value : string.Empty);
                            longitude = ParseCoordinatePartString(lngMatch.Success ? lngMatch.Value : string.Empty);

                            Alert.Message("Latitude =\t" + latitude.coordinateString + ";\tLongitude =\t" + longitude.coordinateString);
                            Alert.Message(latitude.type.ToString() + " =\t" + latitude.decimalCoordinateValue.ToString() + ";\t" +
                                longitude.type.ToString() + " =\t" + longitude.decimalCoordinateValue.ToString());
                        }
                    }
                }
                else if (coordinate.Attributes["type"].InnerText == "latitude")
                {
                    latMatch = Regex.Match(coordinateText, @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    if (latMatch.Success)
                    {
                        if (latMatch.NextMatch().Success)
                        {
                            Alert.Message("WARNING!\n\tMultiple matches of latitude.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            latitude = ParseCoordinatePartString(latMatch.Success ? latMatch.Value : string.Empty);
                            Alert.Message(latitude.type.ToString() + " =\t" + latitude.coordinateString);
                        }
                    }
                }
                else if (coordinate.Attributes["type"].InnerText == "longitude")
                {
                    lngMatch = Regex.Match(coordinateText, @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?");
                    if (lngMatch.Success)
                    {
                        if (lngMatch.NextMatch().Success)
                        {
                            Alert.Message("WARNING!\n\tMultiple matches of longitute.\n\tCurrent coordinate will not be processed!");
                        }
                        else
                        {
                            longitude = ParseCoordinatePartString(lngMatch.Success ? lngMatch.Value : string.Empty);
                            Alert.Message(longitude.type.ToString() + " =\t" + longitude.coordinateString);
                        }
                    }
                }
                Console.WriteLine("{2} =\t{0};\t{3} =\t{1}", latitude.decimalCoordinateValue.ToString("f5"),
                    longitude.decimalCoordinateValue.ToString("f5"), latitude.type.ToString(), longitude.type.ToString());
                Console.WriteLine();

                try
                {
                    if (coordinate.Attributes["latitude"].InnerText == string.Empty && latMatch.Success)
                    {
                        coordinate.Attributes["latitude"].InnerText = latitude.decimalCoordinateValue.ToString("f5");
                    }
                }
                catch (Exception) { }
                try
                {
                    if (coordinate.Attributes["longitude"].InnerText == string.Empty && lngMatch.Success)
                    {
                        coordinate.Attributes["longitude"].InnerText = longitude.decimalCoordinateValue.ToString("f5");
                    }
                }
                catch (Exception) { }
            }

            foreach (XmlNode node in xmlDocument.SelectNodes("//tr[count(.//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)=''])=1][count(.//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!=''])=1]", namespaceManager))
            {
                XmlNode latCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']", namespaceManager);
                XmlNode lngCoordinate = node.SelectSingleNode(".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']", namespaceManager);

                latCoordinate.Attributes["longitude"].InnerText = lngCoordinate.Attributes["longitude"].InnerText;
                lngCoordinate.Attributes["latitude"].InnerText = latCoordinate.Attributes["latitude"].InnerText;
            }

            xml = xmlDocument.OuterXml;
        }

        private CoordinatePart ParseCoordinatePartString(string coordinatePartString)
        {
            CoordinatePart cp = new CoordinatePart();
            cp.coordinateString = Regex.Replace(coordinatePartString, @"\s+", " ");
            cp.coordinateString = Regex.Replace(cp.coordinateString, @"\s+\-\s*|\s*\-\s+", "-");
            if (Regex.Match(cp.coordinateString, @"\-.*?[NSWEO]|[NSWEO].*?\-").Success)
            {
                cp.coordinateString = Regex.Replace(cp.coordinateString, @"\-", "");
            }
            bool N = cp.coordinateString.Contains("N");
            bool S = cp.coordinateString.Contains("S");
            bool E = cp.coordinateString.Contains("E");
            bool W = cp.coordinateString.Contains("W");
            bool O = cp.coordinateString.Contains("O");
            if ((N || S) && !(E || W || O))
            {
                cp.type = CoordinateType.Latitude;
            }
            else if (!(N || S) && (E || W || O))
            {
                cp.type = CoordinateType.Longitude;
            }
            else
            {
                cp.type = CoordinateType.Null;
            }
            // #######################################################################################
            // There is a linguistic problem: O = Ost (German) = East, and O = Oeste (Spanish) = West
            // Here is supposed that O = Ost
            cp.decimalCoordinateSign = S || W ? -1 : 1;
            // #######################################################################################
            cp.coordinateString = Regex.Replace(cp.coordinateString, @"\s*[NSWEO]\s*", "");
            cp.coordinateString = Regex.Replace(Regex.Replace(cp.coordinateString, @"(\d+),(\d+)", "$1.$2"), @"[^\d\.\-]+", " ");
            cp.coordinateString = Regex.Replace(cp.coordinateString, @"(?<=\d)\s+(?=\.)|(?<=\.)\s+(?=\d)", "");
            cp.coordinateString = Regex.Replace(cp.coordinateString, @"\A(.*?\d+)\.(\d+\.\d+.*)\Z", "$1 $2");
            cp.decimalCoordinateValue = cp.decimalCoordinateSign * ParseOneCoordinate(cp.coordinateString);
            return cp;
        }

        private double ParseOneCoordinate(string str)
        {
            double deg, mm, ss;
            string _deg, _mm, _ss;
            if (Regex.Match(str, @"^.*?(\-?\d+(\.\d+)?).*$").Success)
            {
                _deg = Regex.Replace(str, @"^.*?(\-?\d+(\.\d+)?).*$", "$1");
            }
            else
            {
                _deg = " ";
            }
            if (Regex.Match(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$").Success)
            {
                _mm = Regex.Replace(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$2");
            }
            else
            {
                _mm = " ";
            }
            if (Regex.Match(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$").Success)
            {
                _ss = Regex.Replace(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$3");
            }
            else
            {
                _ss = " ";
            }

            Alert.Message("DEG: " + Regex.Replace(_deg, "\\s+", "#") + " MM: " + Regex.Replace(_mm, "\\s+", "#") + " SS: " + Regex.Replace(_ss, "\\s+", "#"));

            try
            {
                deg = _deg.Contains(" ") ? 0.0 : double.Parse(_deg);
                mm = _mm.Contains(" ") ? 0.0 : double.Parse(_mm);
                ss = _ss.Contains(" ") ? 0.0 : double.Parse(_ss);

                return deg + (mm + ss / 60.0) / 60.0;
            }
            catch (System.ArgumentNullException)
            {
                Alert.Message("ArgumentNullException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (System.FormatException)
            {
                Alert.Message("FormatException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (System.OverflowException)
            {
                Alert.Message("OverflowException in Coordinate parameter = " + str);
                return 0.0;
            }
        }

        public enum CoordinateType
        {
            Latitude,
            Longitude,
            Null
        }

        public struct CoordinatePart
        {
            public int decimalCoordinateSign;
            public double decimalCoordinateValue;
            public string coordinateString;
            public CoordinateType type;
        }
    }
}
