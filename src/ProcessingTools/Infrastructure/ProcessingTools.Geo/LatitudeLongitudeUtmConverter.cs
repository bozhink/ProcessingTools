// <copyright file="LatitudeLongitudeUtmConverter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Geo
{
    using System;

    /// <summary>
    /// Latitude-Longitude UTM Converter
    /// </summary>
    /// <remarks>
    /// <para>Adapted from https://github.com/owaremx/LatLngUTMConverter/blob/master/LatLngUTMConverter.cs </para>
    /// <para>JavaScript source: https://github.com/shahid28/utm-latlng/blob/master/UTMLatLng.js </para>
    /// </remarks>
    public class LatitudeLongitudeUtmConverter
    {
        private readonly string datumName = "WGS 84";
        private double axd;
        private double eccSquared;
        private bool status;

        /// <summary>
        /// Initializes a new instance of the <see cref="LatitudeLongitudeUtmConverter"/> class.
        /// </summary>
        public LatitudeLongitudeUtmConverter()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatitudeLongitudeUtmConverter"/> class.
        /// </summary>
        /// <param name="datumName">Datum name.</param>
        public LatitudeLongitudeUtmConverter(string datumName)
        {
            if (!string.IsNullOrEmpty(datumName))
            {
                this.datumName = datumName;
            }

            this.SetEllipsoid(this.datumName);
        }

        /// <summary>
        /// Converts decimal latitude-longitude pair to UTM.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        /// <returns>Calculated UTM.</returns>
        public UtmResult Convert(double latitude, double longitude)
        {
            if (this.status)
            {
                throw new InvalidOperationException("No ellipsoid data associated with unknown datum: " + this.datumName);
            }

            int zoneNumber;

            var longTemp = longitude;
            var latRad = this.ToRadians(latitude);
            var longRad = this.ToRadians(longTemp);

            if (longTemp >= 8 && longTemp <= 13 && latitude > 54.5 && latitude < 58)
            {
                zoneNumber = 32;
            }
            else if (latitude >= 56.0 && latitude < 64.0 && longTemp >= 3.0 && longTemp < 12.0)
            {
                zoneNumber = 32;
            }
            else
            {
                zoneNumber = (int)((longTemp + 180) / 6) + 1;

                if (latitude >= 72.0 && latitude < 84.0)
                {
                    if (longTemp >= 0.0 && longTemp < 9.0)
                    {
                        zoneNumber = 31;
                    }
                    else if (longTemp >= 9.0 && longTemp < 21.0)
                    {
                        zoneNumber = 33;
                    }
                    else if (longTemp >= 21.0 && longTemp < 33.0)
                    {
                        zoneNumber = 35;
                    }
                    else if (longTemp >= 33.0 && longTemp < 42.0)
                    {
                        zoneNumber = 37;
                    }
                }
            }

            var longOrigin = ((zoneNumber - 1) * 6) - 180 + 3; // +3 puts origin in middle of zone
            var longOriginRad = this.ToRadians(longOrigin);

            var utmZone = this.GetUtmLetterDesignator(latitude);

            var eccPrimeSquared = this.eccSquared / (1 - this.eccSquared);

            var n = this.axd / Math.Sqrt(1 - (this.eccSquared * Math.Sin(latRad) * Math.Sin(latRad)));
            var t = Math.Tan(latRad) * Math.Tan(latRad);
            var c = eccPrimeSquared * Math.Cos(latRad) * Math.Cos(latRad);
            var a = Math.Cos(latRad) * (longRad - longOriginRad);

            var m = this.axd * (((1 - (this.eccSquared / 4) - (3 * this.eccSquared * this.eccSquared / 64) - (5 * this.eccSquared * this.eccSquared * this.eccSquared / 256)) * latRad)
                    - (((3 * this.eccSquared / 8) + (3 * this.eccSquared * this.eccSquared / 32) + (45 * this.eccSquared * this.eccSquared * this.eccSquared / 1024)) * Math.Sin(2 * latRad))
                    + (((15 * this.eccSquared * this.eccSquared / 256) + (45 * this.eccSquared * this.eccSquared * this.eccSquared / 1024)) * Math.Sin(4 * latRad))
                    - ((35 * this.eccSquared * this.eccSquared * this.eccSquared / 3072) * Math.Sin(6 * latRad)));

            var utmEasting = (0.9996 * n * (a + ((1 - t + c) * a * a * a / 6)
                    + ((5 - (18 * t) + (t * t) + (72 * c) - (58 * eccPrimeSquared)) * a * a * a * a * a / 120)))
                    + 500000.0;

            var utmNorthing = 0.9996 * (m + (n * Math.Tan(latRad) * ((a * a / 2) + ((5 - t + (9 * c) + (4 * c * c)) * a * a * a * a / 24)
                    + ((61 - (58 * t) + (t * t) + (600 * c) - (330 * eccPrimeSquared)) * a * a * a * a * a * a / 720))));

            if (latitude < 0)
            {
                utmNorthing += 10000000.0;
            }

            return new UtmResult
            {
                Easting = utmEasting,
                Northing = utmNorthing,
                ZoneNumber = zoneNumber,
                ZoneLetter = utmZone
            };
        }

        /// <summary>
        /// Converts UTM to latitude-longitude pair.
        /// </summary>
        /// <param name="utmEasting">UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="utmZoneNumber">UTM Zone Number.</param>
        /// <param name="utmZoneLetter">UTM Zone Letter.</param>
        /// <returns>Calculated latitude-longitude pair.</returns>
        public LatitudeLongitudePair Convert(double utmEasting, double utmNorthing, int utmZoneNumber, string utmZoneLetter)
        {
            bool southern = utmZoneLetter.ToUpperInvariant()[0] < 'N';
            return this.Convert(utmEasting: utmEasting, utmNorthing: utmNorthing, utmZoneNumber: utmZoneNumber, southern: southern);
        }

        /// <summary>
        /// Converts UTM to latitude-longitude pair.
        /// </summary>
        /// <param name="utmEasting">UTM Easting.</param>
        /// <param name="utmNorthing">UTM Northing.</param>
        /// <param name="utmZoneNumber">UTM Zone Number.</param>
        /// <param name="southern">Southern hemisphere or not.</param>
        /// <returns>Calculated latitude-longitude pair.</returns>
        public LatitudeLongitudePair Convert(double utmEasting, double utmNorthing, int utmZoneNumber, bool southern)
        {
            var e1 = (1 - Math.Sqrt(1 - this.eccSquared)) / (1 + Math.Sqrt(1 - this.eccSquared));
            var x = utmEasting - 500000.0; // remove 500,000 meter offset for longitude
            var y = utmNorthing;
            var zoneNumber = utmZoneNumber;
            ////int NorthernHemisphere;

            if (southern)
            {
                y -= 10000000.0;
            }

            var longitudeOrigin = ((zoneNumber - 1) * 6) - 180 + 3;

            var eccPrimeSquared = this.eccSquared / (1 - this.eccSquared);

            double m = y / 0.9996;
            var mu = m / (this.axd * (1 - (this.eccSquared / 4) - (3 * this.eccSquared * this.eccSquared / 64) - (5 * this.eccSquared * this.eccSquared * this.eccSquared / 256)));

            var phi1Rad = mu + (((3 * e1 / 2) - (27 * e1 * e1 * e1 / 32)) * Math.Sin(2 * mu))
                    + (((21 * e1 * e1 / 16) - (55 * e1 * e1 * e1 * e1 / 32)) * Math.Sin(4 * mu))
                    + ((151 * e1 * e1 * e1 / 96) * Math.Sin(6 * mu));
            ////var phi1 = this.ToDegrees(phi1Rad);

            var n1 = this.axd / Math.Sqrt(1 - (this.eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad)));
            var t1 = Math.Tan(phi1Rad) * Math.Tan(phi1Rad);
            var c1 = eccPrimeSquared * Math.Cos(phi1Rad) * Math.Cos(phi1Rad);
            var r1 = this.axd * (1 - this.eccSquared) / Math.Pow(1 - (this.eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad)), 1.5);
            var d = x / (n1 * 0.9996);

            var latitude = phi1Rad - ((n1 * Math.Tan(phi1Rad) / r1) * ((d * d / 2) - ((5 + (3 * t1) + (10 * c1) - (4 * c1 * c1) - (9 * eccPrimeSquared)) * d * d * d * d / 24)
                    + ((61 + (90 * t1) + (298 * c1) + (45 * t1 * t1) - (252 * eccPrimeSquared) - (3 * c1 * c1)) * d * d * d * d * d * d / 720)));
            latitude = this.ToDegrees(latitude);

            var longitude = (d - ((1 + (2 * t1) + c1) * d * d * d / 6) + ((5 - (2 * c1) + (28 * t1) - (3 * c1 * c1) + (8 * eccPrimeSquared) + (24 * t1 * t1))
                    * d * d * d * d * d / 120)) / Math.Cos(phi1Rad);
            longitude = longitudeOrigin + this.ToDegrees(longitude);

            return new LatitudeLongitudePair
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }

        private string GetUtmLetterDesignator(double latitude)
        {
            if ((latitude <= 84) && (latitude >= 72))
            {
                return "X";
            }
            else if ((latitude < 72) && (latitude >= 64))
            {
                return "W";
            }
            else if ((latitude < 64) && (latitude >= 56))
            {
                return "V";
            }
            else if ((latitude < 56) && (latitude >= 48))
            {
                return "U";
            }
            else if ((latitude < 48) && (latitude >= 40))
            {
                return "T";
            }
            else if ((latitude < 40) && (latitude >= 32))
            {
                return "S";
            }
            else if ((latitude < 32) && (latitude >= 24))
            {
                return "R";
            }
            else if ((latitude < 24) && (latitude >= 16))
            {
                return "Q";
            }
            else if ((latitude < 16) && (latitude >= 8))
            {
                return "P";
            }
            else if ((latitude < 8) && (latitude >= 0))
            {
                return "N";
            }
            else if ((latitude < 0) && (latitude >= -8))
            {
                return "M";
            }
            else if ((latitude < -8) && (latitude >= -16))
            {
                return "L";
            }
            else if ((latitude < -16) && (latitude >= -24))
            {
                return "K";
            }
            else if ((latitude < -24) && (latitude >= -32))
            {
                return "J";
            }
            else if ((latitude < -32) && (latitude >= -40))
            {
                return "H";
            }
            else if ((latitude < -40) && (latitude >= -48))
            {
                return "G";
            }
            else if ((latitude < -48) && (latitude >= -56))
            {
                return "F";
            }
            else if ((latitude < -56) && (latitude >= -64))
            {
                return "E";
            }
            else if ((latitude < -64) && (latitude >= -72))
            {
                return "D";
            }
            else if ((latitude < -72) && (latitude >= -80))
            {
                return "C";
            }
            else
            {
                return "Z";
            }
        }

        private void SetEllipsoid(string name)
        {
            switch (name)
            {
                case "Airy":
                    this.axd = 6377563;
                    this.eccSquared = 0.00667054;
                    break;

                case "Australian National":
                case "South American 1969":
                    this.axd = 6378160;
                    this.eccSquared = 0.006694542;
                    break;

                case "Bessel 1841":
                    this.axd = 6377397;
                    this.eccSquared = 0.006674372;
                    break;

                case "Bessel 1841 Nambia":
                    this.axd = 6377484;
                    this.eccSquared = 0.006674372;
                    break;

                case "Clarke 1866":
                    this.axd = 6378206;
                    this.eccSquared = 0.006768658;
                    break;

                case "Clarke 1880":
                    this.axd = 6378249;
                    this.eccSquared = 0.006803511;
                    break;

                case "Everest":
                    this.axd = 6377276;
                    this.eccSquared = 0.006637847;
                    break;

                case "Fischer 1960 Mercury":
                    this.axd = 6378166;
                    this.eccSquared = 0.006693422;
                    break;

                case "Fischer 1968":
                    this.axd = 6378150;
                    this.eccSquared = 0.006693422;
                    break;

                case "GRS 1967":
                    this.axd = 6378160;
                    this.eccSquared = 0.006694605;
                    break;

                case "GRS 1980":
                case "WGS 84":
                case "EUREF89": // Max deviation from WGS 84 is 40 cm/km see http://ocq.dk/euref89 (in danish)
                case "ETRS89": // Same as EUREF89
                    this.axd = 6378137;
                    this.eccSquared = 0.00669438;
                    break;

                case "Helmert 1906":
                    this.axd = 6378200;
                    this.eccSquared = 0.006693422;
                    break;

                case "Hough":
                    this.axd = 6378270;
                    this.eccSquared = 0.00672267;
                    break;

                case "International":
                case "ED50":
                    this.axd = 6378388;
                    this.eccSquared = 0.00672267;
                    break;

                case "Krassovsky":
                    this.axd = 6378245;
                    this.eccSquared = 0.006693422;
                    break;

                case "Modified Airy":
                    this.axd = 6377340;
                    this.eccSquared = 0.00667054;
                    break;

                case "Modified Everest":
                    this.axd = 6377304;
                    this.eccSquared = 0.006637847;
                    break;

                case "Modified Fischer 1960":
                    this.axd = 6378155;
                    this.eccSquared = 0.006693422;
                    break;

                case "WGS 60":
                    this.axd = 6378165;
                    this.eccSquared = 0.006693422;
                    break;

                case "WGS 66":
                    this.axd = 6378145;
                    this.eccSquared = 0.006694542;
                    break;

                case "WGS 72":
                    this.axd = 6378135;
                    this.eccSquared = 0.006694318;
                    break;

                default:
                    this.status = true;
                    break;
            }
        }

        private double ToDegrees(double rad)
        {
            return rad / Math.PI * 180;
        }

        private double ToRadians(double grad)
        {
            return grad * Math.PI / 180;
        }

        /// <summary>
        /// Latitude-Longitude pair
        /// </summary>
        public class LatitudeLongitudePair
        {
            /// <summary>
            /// Gets or sets the latitude.
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Gets or sets the longitude.
            /// </summary>
            public double Longitude { get; set; }
        }

        /// <summary>
        /// UTM Result
        /// </summary>
        public class UtmResult
        {
            /// <summary>
            /// Gets or sets the easting.
            /// </summary>
            public double Easting { get; set; }

            /// <summary>
            /// Gets or sets the northing.
            /// </summary>
            public double Northing { get; set; }

            /// <summary>
            /// Gets or sets the UTM easting.
            /// </summary>
            public double UTMEasting { get; set; }

            /// <summary>
            /// Gets or sets the UTM northing.
            /// </summary>
            public double UTMNorthing { get; set; }

            /// <summary>
            /// Gets the zone.
            /// </summary>
            public string Zone => this.ZoneNumber + this.ZoneLetter;

            /// <summary>
            /// Gets or sets the zone letter.
            /// </summary>
            public string ZoneLetter { get; set; }

            /// <summary>
            /// Gets or sets the zone number.
            /// </summary>
            public int ZoneNumber { get; set; }

            /// <inheritdoc/>
            public override string ToString()
            {
                return $"{this.ZoneNumber}{this.ZoneLetter} {this.Easting}{this.Northing}";
            }
        }
    }
}
