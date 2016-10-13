namespace ProcessingTools.Geo
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using Types;

    public class CoordinatePart
    {
        private const string DotSignAsDecimalSeparator = ".";
        private const string CoordinatePartDecimalFormat = "f6";
        private readonly string numberDecimalSeparator;

        private string coordinatePartString;
        private int decimalCoordinatePartSign;
        private double decimalCoordinatePartValue;
        private CoordinatePartType type;

        private ILogger logger;

        public CoordinatePart(ILogger logger)
        {
            this.logger = logger;
            this.decimalCoordinatePartSign = 1;
            this.decimalCoordinatePartValue = 0.0;
            this.coordinatePartString = string.Empty;
            this.type = CoordinatePartType.Undefined;

            this.numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            this.PartIsPresent = false;
        }

        public string CoordinatePartString
        {
            get
            {
                return this.coordinatePartString;
            }

            set
            {
                this.coordinatePartString = value;
            }
        }

        public bool PartIsPresent { get; set; }

        public string Value
        {
            get
            {
                return this.decimalCoordinatePartValue
                    .ToString(CoordinatePartDecimalFormat)
                    .Replace(this.numberDecimalSeparator, DotSignAsDecimalSeparator);
            }
        }

        public string Type
        {
            get
            {
                return this.type.ToString();
            }
        }

        public void Parse()
        {
            this.coordinatePartString = this.coordinatePartString
                .RegexReplace(@"\s+", " ")
                .RegexReplace(@"\s+\-\s*|\s*\-\s+", "-");

            if (Regex.IsMatch(this.coordinatePartString, @"\-.*?[NSWEO]|[NSWEO].*?\-"))
            {
                this.coordinatePartString = this.coordinatePartString.RegexReplace(@"\-", string.Empty);
            }

            this.DetermineCoordinatePartTypeAndSign();

            this.coordinatePartString = this.coordinatePartString
                .RegexReplace(@"\s*[NSWEO]\s*", string.Empty)
                .RegexReplace(@"(\d+),(\d+)", "$1.$2")
                .RegexReplace(@"[^\d\.\-]+", " ")
                .RegexReplace(@"(?<=\d)\s+(?=\.)|(?<=\.)\s+(?=\d)", string.Empty)
                .RegexReplace(@"\A(.*?\d+)\.(\d+\.\d+.*)\Z", "$1 $2");

            double coordinatePartUnsignedValue = 0.0;
            try
            {
                coordinatePartUnsignedValue = this.ParseCoordinatePart();
            }
            catch (Exception e)
            {
                // TODO: coordinate part
                this.logger?.Log(e, "CoordinatePart.Parse()");
                coordinatePartUnsignedValue = 0.0;
            }
            finally
            {
                this.decimalCoordinatePartValue = this.decimalCoordinatePartSign * coordinatePartUnsignedValue;
            }
        }

        private void DetermineCoordinatePartTypeAndSign()
        {
            bool hasMinus = this.coordinatePartString.Contains("-");
            bool hasN = this.coordinatePartString.Contains("N");
            bool hasS = this.coordinatePartString.Contains("S");
            bool hasE = this.coordinatePartString.Contains("E");
            bool hasW = this.coordinatePartString.Contains("W");
            bool hasO = this.coordinatePartString.Contains("O");
            if ((hasN || hasS) && !(hasE || hasW || hasO))
            {
                this.type = CoordinatePartType.Latitude;
            }
            else if (!(hasN || hasS) && (hasE || hasW || hasO))
            {
                this.type = CoordinatePartType.Longitude;
            }
            else
            {
                this.type = CoordinatePartType.Undefined;
            }

            // There is a linguistic problem: O = Ost (German) = East, and O = Oeste (Spanish) = West
            // Here is supposed that O = Oeste
            this.decimalCoordinatePartSign = (hasS || hasW || hasO) || (hasMinus && !(hasE || hasN)) ? -1 : 1;
        }

        private double ParseCoordinatePart()
        {
            Regex matchDegreesString = new Regex(@"\A.*?(\d+(?:\.\d+)?).*\Z");
            string degreesString = null;
            if (matchDegreesString.IsMatch(this.coordinatePartString))
            {
                degreesString = matchDegreesString
                    .Replace(this.coordinatePartString, "$1")
                    .Replace(DotSignAsDecimalSeparator, this.numberDecimalSeparator);
            }

            Regex matchMinutesString = new Regex(@"\A.*?(\d+(?:\.\d+)?)\s(\d+(?:\.\d+)?).*\Z");
            string minutesString = null;
            if (matchMinutesString.IsMatch(this.coordinatePartString))
            {
                minutesString = matchMinutesString
                    .Replace(this.coordinatePartString, "$2")
                    .Replace(DotSignAsDecimalSeparator, this.numberDecimalSeparator);
            }

            Regex matchSecondsString = new Regex(@"\A.*(\d+(?:\.\d+)?)\s(\d+(?:\.\d+)?)\s(\d+(?:\.\d+)?).*\Z");
            string secondsString = null;
            if (matchSecondsString.IsMatch(this.coordinatePartString))
            {
                secondsString = matchSecondsString
                    .Replace(this.coordinatePartString, "$3")
                    .Replace(DotSignAsDecimalSeparator, this.numberDecimalSeparator);
            }

            try
            {
                double degrees = degreesString == null ? 0.0 : double.Parse(degreesString);
                double minutes = minutesString == null ? 0.0 : double.Parse(minutesString);
                double seconds = secondsString == null ? 0.0 : double.Parse(secondsString);

                return degrees + ((minutes + (seconds / 60.0)) / 60.0);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception in Coordinate parameter = {this.coordinatePartString}", e);
            }
        }
    }
}