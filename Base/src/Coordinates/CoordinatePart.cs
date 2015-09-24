namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System.Text.RegularExpressions;

    public class CoordinatePart
    {
        private string coordinateString;
        private int decimalCoordinateSign;
        private double decimalCoordinateValue;
        private CoordinateType type;

        private ILogger logger;

        public CoordinatePart(ILogger logger)
        {
            this.logger = logger;
            this.decimalCoordinateSign = 1;
            this.decimalCoordinateValue = 0.0;
            this.coordinateString = string.Empty;
            this.type = CoordinateType.Undefined;
        }

        public string CoordinateString
        {
            get
            {
                return this.coordinateString;
            }
        }

        public string CoordinateValue
        {
            get
            {
                return this.decimalCoordinateValue.ToString("f5");
            }
        }

        public string Type
        {
            get
            {
                return this.type.ToString();
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

            this.DetermineCoordinateTypeAndSign();

            this.coordinateString = Regex.Replace(this.coordinateString, @"\s*[NSWEO]\s*", string.Empty);
            this.coordinateString = Regex.Replace(Regex.Replace(this.coordinateString, @"(\d+),(\d+)", "$1.$2"), @"[^\d\.\-]+", " ");
            this.coordinateString = Regex.Replace(this.coordinateString, @"(?<=\d)\s+(?=\.)|(?<=\.)\s+(?=\d)", string.Empty);
            this.coordinateString = Regex.Replace(this.coordinateString, @"\A(.*?\d+)\.(\d+\.\d+.*)\Z", "$1 $2");
            this.decimalCoordinateValue = this.decimalCoordinateSign * this.ParseOneCoordinate(this.coordinateString);
        }

        private void DetermineCoordinateTypeAndSign()
        {
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
                this.type = CoordinateType.Undefined;
            }

            //// There is a linguistic problem: O = Ost (German) = East, and O = Oeste (Spanish) = West
            //// Here is supposed that O = Ost
            this.decimalCoordinateSign = hasS || hasW ? -1 : 1;
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

            this.logger?.Log("DEG: " + Regex.Replace(degreesString, "\\s+", "#") + " MM: " + Regex.Replace(minutesString, "\\s+", "#") + " SS: " + Regex.Replace(secondsString, "\\s+", "#"));

            try
            {
                deg = degreesString.Contains(" ") ? 0.0 : double.Parse(degreesString);
                mm = minutesString.Contains(" ") ? 0.0 : double.Parse(minutesString);
                ss = secondsString.Contains(" ") ? 0.0 : double.Parse(secondsString);

                return deg + ((mm + (ss / 60.0)) / 60.0);
            }
            catch (System.ArgumentNullException)
            {
                this.logger?.Log("ArgumentNullException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (System.FormatException)
            {
                this.logger?.Log("FormatException in Coordinate parameter = " + str);
                return 0.0;
            }
            catch (System.OverflowException)
            {
                this.logger?.Log("OverflowException in Coordinate parameter = " + str);
                return 0.0;
            }
        }
    }
}