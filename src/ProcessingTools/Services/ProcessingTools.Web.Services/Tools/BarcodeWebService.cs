// <copyright file="BarcodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Tools
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Services.Imaging;
    using ProcessingTools.Contracts.Web.Services.Tools;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Models.Tools.Barcode;

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
            if (model is null)
            {
                return null;
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
            var types = typeof(BarcodeType).EnumToDictionary();

            var viewModel = new BarcodeViewModel(types)
            {
                Width = ImagingConstants.DefaultBarcodeWidth,
                Height = ImagingConstants.DefaultBarcodeHeight,
            };

            return Task.FromResult(viewModel);
        }

        /// <inheritdoc/>
        public Task<BarcodeViewModel> MapToViewModel(BarcodeRequestModel model)
        {
            var types = typeof(BarcodeType).EnumToDictionary();

            var viewModel = new BarcodeViewModel(model?.Type ?? (int)BarcodeType.Unspecified, types)
            {
                Content = model?.Content,
                Width = model?.Width ?? ImagingConstants.DefaultBarcodeWidth,
                Height = model?.Height ?? ImagingConstants.DefaultBarcodeHeight,
            };

            return Task.FromResult(viewModel);
        }
    }
}
