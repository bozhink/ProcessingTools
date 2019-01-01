// <copyright file="TestTrips.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace TaxiFares
{
    using TaxiFares.Models;

    /// <summary>
    /// Test data.
    /// </summary>
    internal static class TestTrips
    {
        /// <summary>
        /// Trip 1.
        /// </summary>
        internal static readonly TaxiTrip Trip1 = new TaxiTrip
        {
            VendorId = "VTS",
            RateCode = "1",
            PassengerCount = 1,
            TripDistance = 10.33f,
            PaymentType = "CSH",
            FareAmount = 0 // predict it. actual = 29.5
        };
    }
}
