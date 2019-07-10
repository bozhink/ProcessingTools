// <copyright file="BarcodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Tools
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Services.Imaging.Contracts;
    using ProcessingTools.Web.Models.Tools.Barcode;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Barcode web service.
    /// </summary>
    public class BarcodeWebService : IBarcodeWebService
    {
        private readonly IBarcodeEncoder encoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeWebService"/> class.
        /// </summary>
        /// <param name="encoder">Barcode encoder.</param>
        public BarcodeWebService(IBarcodeEncoder encoder)
        {
            this.encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
        }

        /// <inheritdoc/>
        public async Task<object> EncodeAsync(BarcodeRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var image = await this.encoder.EncodeBase64Async(
                (BarcodeType)model.Type,
                model.Content,
                model.Width,
                model.Height).ConfigureAwait(false);

            return image;
        }

        /// <inheritdoc/>
        public Task<BarcodeViewModel> GetBarcodeViewModelAsync()
        {
            var viewModel = new BarcodeViewModel
            {
                Width = ImagingConstants.DefaultBarcodeWidth,
                Height = ImagingConstants.DefaultBarcodeHeight,
            };

            return Task.FromResult(viewModel);
        }

        /// <inheritdoc/>
        public Task<BarcodeViewModel> MapToViewModel(BarcodeRequestModel model)
        {
            var viewModel = new BarcodeViewModel(model?.Type ?? 0)
            {
                Content = model?.Content,
                Width = model?.Width ?? ImagingConstants.DefaultBarcodeWidth,
                Height = model?.Height ?? ImagingConstants.DefaultBarcodeHeight,
            };

            return Task.FromResult(viewModel);
        }
    }
}
