namespace ProcessingTools.Geo.Data.Common.Constants
{
    public static class ValidationConstants
    {
        public const int MaximalLengthOfGeoName = 300;

        public const int MaximalLengthOfGeoEpithetName = 100;

        public const int MinimalLengthOfContinentName = 2;
        public const int MaximalLengthOfContinentName = 30;
        public const string ContinentNameRegexPattern = @"^[^<>;:]+$";

        public const int MaximalLengthOfCountryName = 60;
        public const int MaximalLengthOfCallingCode = 20;
        public const int MaximalLengthOfLanguageCode = 10;
        public const int MaximalLengthOfIso639xCode = 3;
        public const string CountryNameRegexPattern = ContinentNameRegexPattern;

        public const int MaximalLengthOfCityName = 60;

        public const int MaximalLengthOfStateName = 60;
        public const int MaximalLengthOfAbbreviatedStateName = 5;

        public const int MaximalLengthOgPostCode = 5;
    }
}