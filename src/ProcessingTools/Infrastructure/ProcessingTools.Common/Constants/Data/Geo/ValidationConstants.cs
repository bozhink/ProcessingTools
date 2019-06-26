// <copyright file="ValidationConstants.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Data.Geo
{
    /// <summary>
    /// Validation constants.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Continent name regex pattern.
        /// </summary>
        public const string ContinentNameRegexPattern = @"^[^<>;:]+$";

        /// <summary>
        /// Country name regex pattern.
        /// </summary>
        public const string CountryNameRegexPattern = ContinentNameRegexPattern;

        /// <summary>
        /// Maximal length of abbreviated name.
        /// </summary>
        public const int MaximalLengthOfAbbreviatedName = 5;

        /// <summary>
        /// Maximal length of calling code.
        /// </summary>
        public const int MaximalLengthOfCallingCode = 20;

        /// <summary>
        /// Maximal length of city name.
        /// </summary>
        public const int MaximalLengthOfCityName = 150;

        /// <summary>
        /// Maximal length of continent name.
        /// </summary>
        public const int MaximalLengthOfContinentName = 30;

        /// <summary>
        /// Maximal length of country name.
        /// </summary>
        public const int MaximalLengthOfCountryName = 60;

        /// <summary>
        /// Maximal length of geo epithet name.
        /// </summary>
        public const int MaximalLengthOfGeoEpithetName = 100;

        /// <summary>
        /// Maximal length of geo name.
        /// </summary>
        public const int MaximalLengthOfGeoName = 300;

        /// <summary>
        /// Maximal length of ISO639x code.
        /// </summary>
        public const int MaximalLengthOfIso639xCode = 3;

        /// <summary>
        /// Maximal length of language code.
        /// </summary>
        public const int MaximalLengthOfLanguageCode = 10;

        /// <summary>
        /// Maximal length of post code.
        /// </summary>
        public const int MaximalLengthOfPostCode = 5;

        /// <summary>
        /// Maximal length of state name.
        /// </summary>
        public const int MaximalLengthOfStateName = 60;

        /// <summary>
        /// Maximal length of synonym name.
        /// </summary>
        public const int MaximalLengthOfSynonymName = 150;

        /// <summary>
        /// Maximal length of user identifier.
        /// </summary>
        public const int MaximalLengthOfUserIdentifier = 128;

        /// <summary>
        /// Minimal length of city name.
        /// </summary>
        public const int MinimalLengthOfCityName = 1;

        /// <summary>
        /// Minimal length of continent name.
        /// </summary>
        public const int MinimalLengthOfContinentName = 2;

        /// <summary>
        /// Minimal length of country name.
        /// </summary>
        public const int MinimalLengthOfCountryName = 2;

        /// <summary>
        /// Minimal length of geo epithet name.
        /// </summary>
        public const int MinimalLengthOfGeoEpithetName = 1;

        /// <summary>
        /// Minimal length of geo name.
        /// </summary>
        public const int MinimalLengthOfGeoName = 1;

        /// <summary>
        /// Minimal length of language code.
        /// </summary>
        public const int MinimalLengthOfLanguageCode = 1;

        /// <summary>
        /// Minimal length of post code.
        /// </summary>
        public const int MinimalLengthOfPostCode = 1;

        /// <summary>
        /// Minimal length of state name.
        /// </summary>
        public const int MinimalLengthOfStateName = 1;

        /// <summary>
        /// Minimal length of synonym name.
        /// </summary>
        public const int MinimalLengthOfSynonymName = 150;

        /// <summary>
        /// Minimal length of user identifier.
        /// </summary>
        public const int MinimalLengthOfUserIdentifier = 1;
    }
}
