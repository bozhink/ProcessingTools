// <copyright file="IBarcodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Web.Models.Tools.Barcode;

namespace ProcessingTools.Contracts.Web.Services.Tools
{
    /// <summary>
    /// Barcode web service.
    /// </summary>
    public interface IBarcodeWebService
    {
        /// <summary>
        /// Gets empty <see cref="BarcodeViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="BarcodeViewModel"/>.</returns>
        Task<BarcodeViewModel> GetBarcodeViewModelAsync();

        /// <summary>
        /// Encodes content to image object.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Encoded image.</returns>
        Task<object> EncodeAsync(BarcodeRequestModel model);

        /// <summary>
        /// Maps request model to view model.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="BarcodeViewModel"/>.</returns>
        Task<BarcodeViewModel> MapToViewModel(BarcodeRequestModel model);
    }
}
