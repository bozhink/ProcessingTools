namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System;
    using System.Text.RegularExpressions;

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
            this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"\s+", " ");
            this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"\s+\-\s*|\s*\-\s+", "-");

            if (Regex.IsMatch(this.coordinatePartString, @"\-.*?[NSWEO]|[NSWEO].*?\-"))
            {
                this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"\-", string.Empty);
            }

            this.DetermineCoordinatePartTypeAndSign();

            this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"\s*[NSWEO]\s*", string.Empty);
            this.coordinatePartString = Regex.Replace(Regex.Replace(this.coordinatePartString, @"(\d+),(\d+)", "$1.$2"), @"[^\d\.\-]+", " ");
            this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"(?<=\d)\s+(?=\.)|(?<=\.)\s+(?=\d)", string.Empty);
            this.coordinatePartString = Regex.Replace(this.coordinatePartString, @"\A(.*?\d+)\.(\d+\.\d+.*)\Z", "$1 $2");
            this.decimalCoordinatePartValue = this.decimalCoordinatePartSign * this.ParseCoordinatePart(this.coordinatePartString);
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
            this.decimalCoordinatePartSign = hasS || hasW ? -1 : 1;
        }

        private double ParseCoordinatePart(string str)
        {
            double degrees, minutes, seconds;
            string degreesString, minutesString, secondsString;
            if (Regex.IsMatch(str, @"^.*?(\-?\d+(\.\d+)?).*$"))
            {
                degreesString = Regex.Replace(str, @"^.*?(\-?\d+(\.\d+)?).*$", "$1");
            }
            else
            {
                degreesString = " ";
            }

            if (Regex.IsMatch(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$"))
            {
                minutesString = Regex.Replace(str, @"^.*?\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$2");
            }
            else
            {
                minutesString = " ";
            }

            if (Regex.IsMatch(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$"))
            {
                secondsString = Regex.Replace(str, @"^.*\d+(\.\d+)?\s\d+(\.\d+)?\s(\d+(\.\d+)?).*$", "$3");
            }
            else
            {
                secondsString = " ";
            }

            this.logger?.Log("DEG: " + Regex.Replace(degreesString, "\\s+", "#") + " MM: " + Regex.Replace(minutesString, "\\s+", "#") + " SS: " + Regex.Replace(secondsString, "\\s+", "#"));

            try
            {
                degrees = degreesString.Contains(" ") ? 0.0 : double.Parse(degreesString);
                minutes = minutesString.Contains(" ") ? 0.0 : double.Parse(minutesString);
                seconds = secondsString.Contains(" ") ? 0.0 : double.Parse(secondsString);

                return degrees + ((minutes + (seconds / 60.0)) / 60.0);
            }
            catch (ArgumentNullException)
            {
                this.logger?.Log("ArgumentNullException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (FormatException)
            {
                this.logger?.Log("FormatException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (OverflowException)
            {
                this.logger?.Log("OverflowException in Coordinate parameter = " + str);
                return 0.0;
            }
        }
    }
}