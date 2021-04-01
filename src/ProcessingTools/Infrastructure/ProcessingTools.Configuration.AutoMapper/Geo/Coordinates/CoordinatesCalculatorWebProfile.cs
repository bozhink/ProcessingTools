// <copyright file="CoordinatesCalculatorWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Geo.Coordinates
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;
    using ProcessingTools.Web.Models.Tools.Coordinates;

    /// <summary>
    /// Coordinates calculator (web) profile.
    /// </summary>
    public class CoordinatesCalculatorWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesCalculatorWebProfile"/> class.
        /// </summary>
        public CoordinatesCalculatorWebProfile()
        {
            this.CreateMap<ICoordinateStringModel, CoordinateViewModel>();
        }
    }
}
