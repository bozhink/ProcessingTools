// <copyright file="Coordinate2DParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Geo.Coordinates
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Models.Geo.Coordinates;

    /// <summary>
    /// 2D coordinate parser.
    /// </summary>
    public class Coordinate2DParser : ICoordinate2DParser
    {
        private const string RepeatedDirectionsErrorMessage = "Repeated directions in the coordinate string.";

        private const string LatitudeTypeValue = AttributeNames.Latitude;
        private const string LongitudeTypeValue = AttributeNames.Longitude;
        private const string UtmZoneValue = "zone";
        private const string UtmEastingValue = "easting";
        private const string UtmNorthingValue = "northing";

        private const string CoordinateParsePattern = @"\A\W*?(\-?\d+[\.,\s]{1,3}\d+(?=\W*\s\W*\-?\d+[\.,\s]{1,3}\d+)|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SNWOE]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W+?((?<=\-?\d+[\.,\s]{1,3}\d+\W*\s\W*?)\-?\d+[\.,\s]{1,3}\d+|\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|[SNWOE]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[SNWOE]?)\W*?\Z";

        private const string LatitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[SN]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string LongitudeMatchPattern = @"\-?\d+\W{1,3}\d+\W{1,3}\d+\W{0,10}?[EWO]|\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,10}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string MatchLongitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[EWO]?|[EWO]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private const string MatchLatitudePartPattern = @"\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*)?)?[NS]?|[NS]\W{0,4}?\-?\d+([,\.]\d+)?°?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?\s*(\d+([,\.]\d+)?\s*(\W{1,2})?)?)?";

        private readonly IUtmCoordinatesTransformer utmCoordinatesTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate2DParser"/> class.
        /// </summary>
        /// <param name="utmCoordinatesTransformer">UTM coordinates </param>
        public Coordinate2DParser(IUtmCoordinatesTransformer utmCoordinatesTransformer)
        {
            this.utmCoordinatesTransformer = utmCoordinatesTransformer ?? throw new ArgumentNullException(nameof(utmCoordinatesTransformer));
        }

        /// <inheritdoc/>
        public void ParseCoordinateString(string coordinateString, string coordinateType, ICoordinatePart latitude, ICoordinatePart longitude)
        {
            try
            {
                {
                    var integerUtmCoordinatesMatchPatterns = new string[]
                    {
                    // UTM WGS84: 33T 455.4683
                    @"\A(?:\s*UTM\s*[A-Z]+[0-9]+:?\s+)?(?<zone>[0-9]{1,2}[A-Z])\s+(?<easting>[0-9]{2,7})\.(?<northing>[0-9]{2,7})\s*\Z",

                    // UTM ED50: 33T 4498003N; 674582E
                    @"\A(?:\s*UTM\s*[A-Z]+[0-9]+:?\s+)?(?<zone>[0-9]{1,2}[A-Z])\s+(?<northing>[0-9]{2,7})N\W+(?<easting>[0-9]{2,7})E\s*\Z",

                    // UTM ED50: 33T 674582E; 4498003N
                    @"\A(?:\s*UTM\s*[A-Z]+[0-9]+:?\s+)?(?<zone>[0-9]{1,2}[A-Z])\s+(?<easting>[0-9]{2,7})E\W+(?<northing>[0-9]{2,7})N\s*\Z",

                    // 55G 595500 5371700
                    @"\A(?:\s*UTM\s*[A-Z]+[0-9]+:?\s+)?(?<zone>[0-9]{1,2}[A-Z])\s+(?<easting>[0-9]{2,6})\W+(?<northing>[0-9]{2,7})\s*\Z"
                    };

                    var integerUtmCoordinatesMatches = integerUtmCoordinatesMatchPatterns.Select(p => Regex.Match(coordinateString, p));
                    foreach (var match in integerUtmCoordinatesMatches)
                    {
                        if (this.ProcessIntegerUTMCoordinate(match, latitude, longitude))
                        {
                            return;
                        }
                    }
                }

                {
                    string coordinateText = coordinateString
                        .RegexReplace("[–—−-]", "-")
                        .RegexReplace(@"\s*:\s*", " ");

                    var simpleSphericalCoordinatesPatterns = new string[]
                    {
                    // 29.5423°, -86.1926°  /see test case/
                    @"\A(?<latitude>\-?[0-9]+\.[0-9]+\W?)[;,\s]+(?<longitude>\-?[0-9]+\.[0-9]+\W?)\Z",

                    // -31:34:55; 159:5:9 /see test case/
                    @"\A(?<latitude>\-?[0-9]+ [0-9]+ [0-9]+)[;,\s]+(?<longitude>\-?[0-9]+ [0-9]+ [0-9]+)\Z",
                    };

                    var simplesphericalCoordinatesMatches = simpleSphericalCoordinatesPatterns.Select(p => Regex.Match(coordinateText, p));
                    foreach (var match in simplesphericalCoordinatesMatches)
                    {
                        if (this.ProcessSimpleSphericalCoordinate(match, latitude, longitude))
                        {
                            return;
                        }
                    }
                }

                {
                    //// 03°14.78S, 72°54.61W /see test case/
                    //// 03°15’S 72°54’W /see test case/
                    //// 20°20.1N 74°33.6W /see test case/
                    //// 37°08'09.4"N, 8°23'04.2"W /see test case/
                    //// 08º48’23’’S, 115º56’24’’E /see test case/

                    string coordinateText = this.SimplifyCoordinateString(coordinateString);

                    if (string.IsNullOrWhiteSpace(coordinateType))
                    {
                        this.ParseGeneralTypeCoordinate(coordinateText, latitude, longitude);
                    }
                    else if (coordinateType == LatitudeTypeValue)
                    {
                        this.ParseLatitudeTypeCoordinate(coordinateText, latitude);
                    }
                    else if (coordinateType == LongitudeTypeValue)
                    {
                        this.ParseLongitudeTypeCoordinate(coordinateText, longitude);
                    }
                }
            }
            catch (CoordinateException)
            {
                string coordinateText = this.SimplifyCoordinateString(coordinateString);
                var latitudeString = Regex.Replace(coordinateText, @"\A.*([NS])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");
                var longitudeString = Regex.Replace(coordinateText, @"\A.*([EW])\W?(\d{1,3})\W{1,3}(\d{1,3})\W{1,3}(\d{1,3}).*\Z", "$1$2 $3 $4");

                this.ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
                    latitudeString,
                    longitudeString,
                    latitude,
                    longitude);
            }
        }

        private bool ProcessSimpleSphericalCoordinate(Match match, ICoordinatePart latitude, ICoordinatePart longitude)
        {
            if (match.Success)
            {
                var latitudeString = match.Groups[LatitudeTypeValue].Value.Trim();
                var longitudeString = match.Groups[LongitudeTypeValue].Value.Trim();

                this.ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
                    latitudeString,
                    longitudeString,
                    latitude,
                    longitude);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Here is supposed that easting and northing are integers, e.g. 33T 4498003N; 674582E, but not 33T 4498003.123N; 674582.3542E
        /// </summary>
        /// <param name="utmCoordinatesMatch">Regex.Match object to match the coordinate parts. Should have named groups "zone", "easting", and "northing".</param>
        /// <param name="latitude">ICoordinatePart object for the latitude coordinate part.</param>
        /// <param name="longitude">ICoordinatePart object for the longitude coordinate part.</param>
        /// <returns>The Success value of the <paramref name="utmCoordinatesMatch"/> match.</returns>
        private bool ProcessIntegerUTMCoordinate(Match utmCoordinatesMatch, ICoordinatePart latitude, ICoordinatePart longitude)
        {
            if (utmCoordinatesMatch.Success)
            {
                // Add tailing zeros
                var utmEastingString = utmCoordinatesMatch.Groups[UtmEastingValue].Value.Trim().PadRight(6, '0');
                var utmNorthingString = utmCoordinatesMatch.Groups[UtmNorthingValue].Value.Trim().PadRight(7, '0');

                var utmZone = utmCoordinatesMatch.Groups[UtmZoneValue].Value.Trim();
                var utmEasting = double.Parse(utmEastingString);
                var utmNorthing = double.Parse(utmNorthingString);

                var point = this.utmCoordinatesTransformer.TransformUtm2Decimal(utmEasting, utmNorthing, utmZone);

                latitude.DecimalValue = point[0];
                latitude.Type = CoordinatePartType.Latitude;
                latitude.PartIsPresent = true;

                longitude.DecimalValue = point[1];
                longitude.Type = CoordinatePartType.Longitude;
                longitude.PartIsPresent = true;

                return true;
            }

            return false;
        }

        private void ParseGeneralTypeCoordinate(string coordinateText, ICoordinatePart latitude, ICoordinatePart longitude)
        {
            var coordinate = new Coordinate();

            string leftPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$1");
            string rightPart = Regex.Replace(coordinateText, CoordinateParsePattern, "$16");

            this.DetermineLatitudeAndLongitudePartsFromTwoPartSeparableCoordinateString(coordinate, leftPart, rightPart);

            this.ParseCoordinateObject(latitude, longitude, coordinate);
        }

        private void DetermineLatitudeAndLongitudePartsFromTwoPartSeparableCoordinateString(ICoordinate coordinate, string leftPart, string rightPart)
        {
            if ((leftPart.Contains("N") || leftPart.Contains("S")) &&
                (rightPart.Contains("E") || rightPart.Contains("W") || rightPart.Contains("O")))
            {
                if (leftPart.Contains("E") || leftPart.Contains("W") || leftPart.Contains("O") ||
                    rightPart.Contains("N") || rightPart.Contains("S"))
                {
                    throw new CoordinateException(RepeatedDirectionsErrorMessage);
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
                    throw new CoordinateException(RepeatedDirectionsErrorMessage);
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

        private void ProcessCoordinateNodeWithDeterminedLatitudeAndLongitudeStringParts(
            string latitudeString,
            string longitudeString,
            ICoordinatePart latitude,
            ICoordinatePart longitude)
        {
            var coordinate = new Coordinate
            {
                Latitude = latitudeString,
                Longitude = longitudeString
            };

            this.ParseCoordinateObject(latitude, longitude, coordinate);
        }

        private void ParseCoordinateObject(ICoordinatePart latitude, ICoordinatePart longitude, ICoordinate coordinate)
        {
            this.ParseSinglePartTypeCoordinate(
                coordinate.Latitude,
                latitude,
                LatitudeMatchPattern);

            this.ParseSinglePartTypeCoordinate(
                coordinate.Longitude,
                longitude,
                LongitudeMatchPattern);
        }

        private void ParseLongitudeTypeCoordinate(string coordinateText, ICoordinatePart longitude)
        {
            this.ParseSinglePartTypeCoordinate(coordinateText, longitude, MatchLongitudePartPattern);
        }

        private void ParseLatitudeTypeCoordinate(string coordinateText, ICoordinatePart latitude)
        {
            this.ParseSinglePartTypeCoordinate(coordinateText, latitude, MatchLatitudePartPattern);
        }

        private void ParseSinglePartTypeCoordinate(string coordinateText, ICoordinatePart coordinatePart, string matchPartPattern)
        {
            Match matchPart = Regex.Match(coordinateText, matchPartPattern);

            coordinatePart.PartIsPresent = matchPart.Success;

            if (coordinatePart.PartIsPresent)
            {
                if (matchPart.NextMatch().Success)
                {
                    throw new CoordinateException($"Multiple matches of {coordinatePart.Type}");
                }
                else
                {
                    coordinatePart.CoordinatePartString = coordinatePart.PartIsPresent ? matchPart.Value : string.Empty;
                    coordinatePart.Parse();
                    //// this.logger?.Log("{0} =\t{1}", coordinatePart.Type, coordinatePart.CoordinatePartString);
                }
            }
        }

        private string SimplifyCoordinateString(string coordinateString)
        {
            string coordinateText = coordinateString
                .RegexReplace(@"'\s*\-+\s*""", "'") //// 16°03'--""S, 130°26'--""E  /see test case/
                .RegexReplace("[–—−-]", "-")
                .RegexReplace(@"[,;]", ",")
                .RegexReplace(@"(?i)[,;:\.\s]+(lat|long?)(\.|itude)", " ") //// Lon. 151. E. Lat. 3. S. /see test case/
                .RegexReplace(@"[^EWONS\d\W]+", " ") //// Remove text
                .RegexReplace(@"\s[a-z]+\s", " ")
                .RegexReplace(@"\-\s+(?=\d)", " -")
                .RegexReplace("E(?=[EWONS])", " ") //// 29.63527EN, 82.37111EW /see test case/
                .RegexReplace(@"[\\\/\|<>\!\?\*:=]+", " ") //// Remove some unused special characters
                .RegexReplace(@"\s{2,}", " ")
                .RegexReplace(@"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9])\s*\.\s*([0-5][0-9](\s*\.\s*\d+)?(?!\.)(?!\d))", "$1 $2 $3") //// N33.50.13, E107.48.52 --> N33 50 13, E107 48 52 /see test case/
                .RegexReplace(@"([01]?[0-9]?[0-9])\s*\.\s*([0-5][0-9]\s*\.\s*[0-9]{3,})", "$1 $2") //// N33.50.613, E107.48.524 --> N33 50.613, E107 48.524 /see test case/
                .RegexReplace(@"(?<=°\s*\d\d)\s+(?=\d\d\d)", ".") //// S39°34 283, W71°29 908 /see test case/
                .RegexReplace(@"(?<=°\s*\d\d)\s*'\s*(\d\d\d)\s*""", ".$1 ") //// S39°34'283"W 71°29'908" /see test case/
                .RegexReplace(@"(?<=\d)(\s*[,\.]\s+|\s+[,\.]\s*)(?=\d)", ".") //// 20. 58139°S, 164.76444°E /see test case/
                .RegexReplace(@"\W*°[^\w,]+|W+°[^\w,]*", "°") //// 22.14158°’S, 166.67993 °E /see test case/
                .RegexReplace(@"(?<=\d)\s+°", "°") //// 164 °7.6444'E /see test case/
                .RegexReplace(@"\A(\W*\d+)\.\W*([EWONS])", "$1.0$2") //// Lon. 151. E. Lat. 3. S. /see test case/
                .RegexReplace(@"([EWONS]\W+\d+)\.\W*([EWONS])", "$1.0$2") //// Lon. 151. E. Lat. 3. S. /see test case/
                .Trim(new[] { ' ', '.', ',', ';' });

            return coordinateText;
        }
    }
}
