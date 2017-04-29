namespace ProcessingTools.Constants.Data.Geo
{
    public sealed class ValidationConstants
    {
        public const string ContinentNameRegexPattern = @"^[^<>;:]+$";
        public const string CountryNameRegexPattern = ContinentNameRegexPattern;
        public const int MaximalLengthOfAbbreviatedStateName = 5;
        public const int MaximalLengthOfCallingCode = 20;
        public const int MaximalLengthOfCityName = 150;
        public const int MaximalLengthOfContinentName = 30;
        public const int MaximalLengthOfCountryName = 60;
        public const int MaximalLengthOfGeoEpithetName = 100;
        public const int MaximalLengthOfGeoName = 300;
        public const int MaximalLengthOfIso639xCode = 3;
        public const int MaximalLengthOfLanguageCode = 10;
        public const int MaximalLengthOfPostCode = 5;
        public const int MaximalLengthOfStateName = 60;
        public const int MaximalLengthOfUserIdentifier = 128;
        public const int MinimalLengthOfCityName = 1;
        public const int MinimalLengthOfContinentName = 2;
        public const int MinimalLengthOfCountryName = 2;
        public const int MinimalLengthOfGeoEpithetName = 1;
        public const int MinimalLengthOfGeoName = 1;
        public const int MinimalLengthOfLanguageCode = 1;
        public const int MinimalLengthOfPostCode = 1;
        public const int MinimalLengthOfStateName = 1;
        public const int MinimalLengthOfUserIdentifier = 1;
    }
}
