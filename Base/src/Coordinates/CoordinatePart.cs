namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System;
    using System.Text.RegularExpressions;
    using Globals;
    using Globals.Loggers;

    public class CoordinatePart
    {
        private const string CoordinatePartDecimalFormat = "f6";

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
                return this.decimalCoordinatePartValue.ToString(CoordinatePartDecimalFormat);
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

            //// There is a linguistic problem: O = Ost (German) = East, and O = Oeste (Spanish) = West
            //// Here is supposed that O = Ost
            this.decimalCoordinatePartSign = (hasS || hasW) ? -1 : 1;
        }

        private double ParseCoordinatePart()
        {
            double degrees, minutes, seconds;
            string degreesString, minutesString, secondsString;

            Regex matchDegreesString = new Regex(@"^.*?(\-?\d+(\.\d+)?).*$");
            if (matchDegreesString.IsMatch(this.coordinatePartString))
            {
                degreesString = matchDegreesString.Replace(this.coordinatePartString, "$1");
            }
            else
            {
                degreesString = " ";
            }

            Regex matchMinutesString = new Regex(@"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$");
            if (matchMinutesString.IsMatch(this.coordinatePartString))
            {
                minutesString = matchMinutesString.Replace(this.coordinatePartString, "$2");
            }
            else
            {
                minutesString = " ";
            }

            Regex matchSecondsString = new Regex(@"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$");
            if (matchSecondsString.IsMatch(this.coordinatePartString))
            {
                secondsString = matchSecondsString.Replace(this.coordinatePartString, "$3");
            }
            else
            {
                secondsString = " ";
            }

            //// this.logger?.Log($"DEG: {Regex.Replace(degreesString, "\\s+", "#")} MM: {Regex.Replace(minutesString, "\\s+", "#")} SS: {Regex.Replace(secondsString, "\\s+", "#")}");

            try
            {
                degrees = degreesString.Contains(" ") ? 0.0 : double.Parse(degreesString);
                minutes = minutesString.Contains(" ") ? 0.0 : double.Parse(minutesString);
                seconds = secondsString.Contains(" ") ? 0.0 : double.Parse(secondsString);

                return degrees + ((minutes + (seconds / 60.0)) / 60.0);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception in Coordinate parameter = {this.coordinatePartString}", e);
            }
        }
    }
}