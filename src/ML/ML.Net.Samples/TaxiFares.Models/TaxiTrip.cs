// <copyright file="TaxiTrip.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace TaxiFares.Models
{
    using Microsoft.ML.Runtime.Api;

    /// <summary>
    /// TaxiTrip is the input data class and has definitions for each of the data set columns. Use the Column attribute to specify the indices of the source columns in the data set.
    /// </summary>
    public class TaxiTrip
    {
        /// <summary>
        /// Gets or sets the ID of the taxi vendor (feature).
        /// </summary>
        [Column("0")]
        public string VendorId { get; set; }

        /// <summary>
        /// Gets or sets the rate type of the taxi trip (feature).
        /// </summary>
        [Column("1")]
        public string RateCode { get; set; }

        /// <summary>
        /// Gets or sets the number of passengers on the trip (feature).
        /// </summary>
        [Column("2")]
        public float PassengerCount { get; set; }

        /// <summary>
        /// Gets or sets the amount of time the trip took.
        /// </summary>
        /// <remarks>
        /// You want to predict the fare of the trip before the trip is completed. At that moment you don't know how long the trip would take. Thus, the trip time is not a feature and you'll exclude this column from the model.
        /// </remarks>
        [Column("3")]
        public float TripTime { get; set; }

        /// <summary>
        /// Gets or sets the distance of the trip (feature).
        /// </summary>
        [Column("4")]
        public float TripDistance { get; set; }

        /// <summary>
        /// Gets or sets the payment method (cash or credit card) (feature).
        /// </summary>
        [Column("5")]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the total taxi fare paid (label).
        /// </summary>
        [Column("6")]
        public float FareAmount { get; set; }
    }
}
