// <copyright file="ValidationConstants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants.Data.Geo
{
    /// <summary>
    /// Validation constants
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// ContinentNameRegexPattern
        /// </summary>
        public const string ContinentNameRegexPattern = @"^[^<>;:]+$";

        /// <summary>
        /// CountryNameRegexPattern
        /// </summary>
        public const string CountryNameRegexPattern = ContinentNameRegexPattern;

        /// <summary>
        /// MaximalLengthOfAbbreviatedName
        /// </summary>
        public const int MaximalLengthOfAbbreviatedName = 5;

        /// <summary>
        /// MaximalLengthOfCallingCode
        /// </summary>
        public const int MaximalLengthOfCallingCode = 20;

        /// <summary>
        /// MaximalLengthOfCityName
        /// </summary>
        public const int MaximalLengthOfCityName = 150;

        /// <summary>
        /// MaximalLengthOfContinentName
        /// </summary>
        public const int MaximalLengthOfContinentName = 30;

        /// <summary>
        /// MaximalLengthOfCountryName
        /// </summary>
        public const int MaximalLengthOfCountryName = 60;

        /// <summary>
        /// MaximalLengthOfGeoEpithetName
        /// </summary>
        public const int MaximalLengthOfGeoEpithetName = 100;

        /// <summary>
        /// MaximalLengthOfGeoName
        /// </summary>
        public const int MaximalLengthOfGeoName = 300;

        /// <summary>
        /// MaximalLengthOfIso639xCode
        /// </summary>
        public const int MaximalLengthOfIso639xCode = 3;

        /// <summary>
        /// MaximalLengthOfLanguageCode
        /// </summary>
        public const int MaximalLengthOfLanguageCode = 10;

        /// <summary>
        /// MaximalLengthOfPostCode
        /// </summary>
        public const int MaximalLengthOfPostCode = 5;

        /// <summary>
        /// MaximalLengthOfStateName
        /// </summary>
        public const int MaximalLengthOfStateName = 60;

        /// <summary>
        /// MaximalLengthOfSynonymName
        /// </summary>
        public const int MaximalLengthOfSynonymName = 150;

        /// <summary>
        /// MaximalLengthOfUserIdentifier
        /// </summary>
        public const int MaximalLengthOfUserIdentifier = 128;

        /// <summary>
        /// MinimalLengthOfCityName
        /// </summary>
        public const int MinimalLengthOfCityName = 1;

        /// <summary>
        /// MinimalLengthOfContinentName
        /// </summary>
        public const int MinimalLengthOfContinentName = 2;

        /// <summary>
        /// MinimalLengthOfCountryName
        /// </summary>
        public const int MinimalLengthOfCountryName = 2;

        /// <summary>
        /// MinimalLengthOfGeoEpithetName
        /// </summary>
        public const int MinimalLengthOfGeoEpithetName = 1;

        /// <summary>
        /// MinimalLengthOfGeoName
        /// </summary>
        public const int MinimalLengthOfGeoName = 1;

        /// <summary>
        /// MinimalLengthOfLanguageCode
        /// </summary>
        public const int MinimalLengthOfLanguageCode = 1;

        /// <summary>
        /// MinimalLengthOfPostCode
        /// </summary>
        public const int MinimalLengthOfPostCode = 1;

        /// <summary>
        /// MinimalLengthOfStateName
        /// </summary>
        public const int MinimalLengthOfStateName = 1;

        /// <summary>
        /// MinimalLengthOfSynonymName
        /// </summary>
        public const int MinimalLengthOfSynonymName = 150;

        /// <summary>
        /// MinimalLengthOfUserIdentifier
        /// </summary>
        public const int MinimalLengthOfUserIdentifier = 1;
    }
}
