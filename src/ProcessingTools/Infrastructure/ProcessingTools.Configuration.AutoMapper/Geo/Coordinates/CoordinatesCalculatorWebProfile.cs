// <copyright file="CoordinatesCalculatorWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Geo.Coordinates;

namespace ProcessingTools.Configuration.AutoMapper.Geo.Coordinates
{
    using global::AutoMapper;
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
