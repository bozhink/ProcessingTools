﻿// <copyright file="CoordinatesCalculatorWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Geo.Coordinates
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo.Coordinates;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;
    using ProcessingTools.Contracts.Web.Services.Geo.Coordinates;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Models.Tools.Coordinates;

    /// <summary>
    /// Coordinates calculator web presenter.
    /// </summary>
    public class CoordinatesCalculatorWebService : ICoordinatesCalculatorWebService
    {
        private readonly ICoordinatesParseService coordinatesParseService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesCalculatorWebService"/> class.
        /// </summary>
        /// <param name="coordinatesParseService">Instance of <see cref="ICoordinatesParseService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public CoordinatesCalculatorWebService(ICoordinatesParseService coordinatesParseService, IMapper mapper, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.coordinatesParseService = coordinatesParseService ?? throw new ArgumentNullException(nameof(coordinatesParseService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public async Task<CoordinatesViewModel> GetCoordinatesViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new CoordinatesViewModel(userContext, Array.Empty<CoordinateViewModel>());
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<CoordinatesViewModel> ParseCoordinatesAsync(CoordinatesRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model == null || string.IsNullOrWhiteSpace(model.Coordinates))
            {
                return new CoordinatesViewModel(userContext, Array.Empty<CoordinateViewModel>())
                {
                    ReturnUrl = model?.ReturnUrl,
                };
            }

            var parsedCoordinates = await this.coordinatesParseService.ParseCoordinatesStringAsync(model.Coordinates).ConfigureAwait(false);
            if (parsedCoordinates == null || !parsedCoordinates.Any())
            {
                return new CoordinatesViewModel(userContext, Array.Empty<CoordinateViewModel>())
                {
                    ReturnUrl = model?.ReturnUrl,
                };
            }

            // TODO: log exceptions.
            var coordinates = parsedCoordinates.Select(this.mapper.Map<ICoordinateStringModel, CoordinateViewModel>).ToArray();

            return new CoordinatesViewModel(userContext, coordinates)
            {
                ReturnUrl = model?.ReturnUrl,
            };
        }
    }
}