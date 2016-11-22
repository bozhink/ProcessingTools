namespace ProcessingTools.Geo
{
    using System;
    using System.Text.RegularExpressions;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;

    public class Coordinate2DParser
    {
        private const string RepeatedDirectionsErrorMessage = "Repeated directions in the coordinate string.";

        private const string LatitudeTypeValue = AttributeNames.Latitude;
        private const string LongitudeTypeValue = AttributeNames.Longitude;

        // TODO: Error on 34.47325°, 132.10362°
        private const string CoordinateParsePattern = @"\A\W*?(\-?\d+[\.,\s]{1,3}\d+(?=\W*\s\W*\-?\d+[\.,\s]{1,3}\d+)|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SNWOE]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W+?((?<=\-?\d+[\.,\s]{1,3}\d+\W*\s\W*?)\-?\d+[\.,\s]{1,3}\d+|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W*?\Z";

        private const string LatitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string LongitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string MatchLongitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string MatchLatitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        public static void ParseCoordinateString(string coordinateString, string coordinateType, CoordinatePart latitude, CoordinatePart longitude)
        {
            string coordinateText = SimplifyCoordinateString(coordinateString);

            try
            {
                // 29.5423°, -86.1926°
                Regex matchDecimalDecimalCoordinateRegex = new Regex(@"\A(?<latitude>\W?\d+\.\d+\W?)[,\s]+(?<longitude>\W?\d+\.\d+\W?)\Z");
                if (matchDecimalDecimalCoordinateRegex.IsMatch(coordinateText))
                {
                    Match matchDecimalDecimalCoordinate = matchDecimalDecimalCoordinateRegex.Match(coordinateText);
                    var latitudeString = matchDecimalDecimalCoordinate.Groups[AttributeNames.Latitude].Value.Trim();
                    var longitudeString = matchDecimalDecimalCoordinate.Groups[AttributeNames.Longitude].Value.Trim();

                    ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
                        latitudeString,
                        longitudeString,
                        latitude,
                        longitude);
                }
                else
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

                    if (string.IsNullOrWhiteSpace(coordinateType))
                    {
                        ParseGeneralTypeCoordinate(coordinateText, latitude, longitude);
                    }
                    else if (coordinateType == LatitudeTypeValue)
                    {
                        ParseLatitudeTypeCoordinate(coordinateText, latitude);
                    }
                    else if (coordinateType == LongitudeTypeValue)
                    {
                        ParseLongitudeTypeCoordinate(coordinateText, longitude);
                    }
                }
            }
            catch (ApplicationException)
            {
                var latitudeString = Regex.Replace(coordinateText, @"\A.*([NS])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");
                var longitudeString = Regex.Replace(coordinateText, @"\A.*([EW])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");

                ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
                    latitudeString,
                    longitudeString,
                    latitude,
                    longitude);
            }
        }

        private static void ParseGeneralTypeCoordinate(string coordinateText, CoordinatePart latitude, CoordinatePart longitude)
        {
            var coordinate = new Coordinate();

            {
                string leftPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$1");
                string rightPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$16");

                DetermineLatitudeAndLongitudePartsFromTwoPartSeparableCoordinateString(coordinate, leftPart, rightPart);
            }

            ParseCoordinateObject(latitude, longitude, coordinate);
        }

        private static void DetermineLatitudeAndLongitudePartsFromTwoPartSeparableCoordinateString(Coordinate coordinate, string leftPart, string rightPart)
        {
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
        }

        private static void ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
            string latitudeString,
            string longitudeString,
            CoordinatePart latitude,
            CoordinatePart longitude)
        {
            var coordinate = new Coordinate
            {
                Latitude = latitudeString,
                Longitude = longitudeString
            };

            ParseCoordinateObject(latitude, longitude, coordinate);
        }

        private static void ParseCoordinateObject(CoordinatePart latitude, CoordinatePart longitude, Coordinate coordinate)
        {
            ParseSinglePartTypeCoordinate(
                coordinate.Latitude,
                latitude,
                LatitudeMatchPattern);

            ParseSinglePartTypeCoordinate(
                coordinate.Longitude,
                longitude,
                LongitudeMatchPattern);
        }

        private static void ParseLongitudeTypeCoordinate(string coordinateText, CoordinatePart longitude)
        {
            ParseSinglePartTypeCoordinate(coordinateText, longitude, MatchLongitudePartPattern);
        }

        private static void ParseLatitudeTypeCoordinate(string coordinateText, CoordinatePart latitude)
        {
            ParseSinglePartTypeCoordinate(coordinateText, latitude, MatchLatitudePartPattern);
        }

        private static void ParseSinglePartTypeCoordinate(string coordinateText, CoordinatePart coordinatePart, string matchPartPattern)
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

        private static string SimplifyCoordinateString(string coordinateString)
        {
            string coordinateText = coordinateString
                .RegexReplace("[–—−-]", "-")
                .RegexReplace(@"[,;]", ",")
                .RegexReplace(@"[^EWONS\d\W]+", " ") //// Remove text
                .RegexReplace(@"\s[a-z]+\s", " ")
                .RegexReplace(@"\-\s+(?=\d)", " -")
                .RegexReplace("E(?=[EWONS])", " ") //// 29.63527EN, 82.37111EW
                .RegexReplace(@"[\\\/\|<>\!\?\*:=]+", " ") //// Remove some unused special characters
                .RegexReplace(@"\s{2,}", " ")
                .RegexReplace(@"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9])\s*\.\s*([0-5][0-9](\s*\.\s*\d+)?(?!\.)(?!\d))", "$1 $2 $3") //// N33.50.13, E107.48.52 --> N33 50 13, E107 48 52
                .RegexReplace(@"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9]\s*\.\s*[0-9]{3,})", "$1 $2") //// N33.50.613, E107.48.524 --> N33 50.613, E107 48.524
                .RegexReplace(@"(?<=°\s*\d\d)\s+(?=\d\d\d)", ".") //// S39°34 283, W71°29 908
                .RegexReplace(@"(?<=°\s*\d\d)\s*'\s*(\d\d\d)\s*""", ".$1 ") //// S39°34'283"W 71°29'908"
                .RegexReplace(@"(?<=\d)(\s*[,\.]\s+|\s+[,\.]\s*)(?=\d)", ".") //// 20. 58139°S, 164.76444°E
                .RegexReplace(@"\W*°[^\w,]+|W+°[^\w,]*", "°"); //// 22.14158°’S, 166.67993 °E

            return coordinateText;
        }
    }
}
