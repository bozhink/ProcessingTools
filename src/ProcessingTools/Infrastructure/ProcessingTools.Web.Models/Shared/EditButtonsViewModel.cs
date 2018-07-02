// <copyright file="EditButtonsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    /// <summary>
    /// Edit buttons view model
    /// </summary>
    public class EditButtonsViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the cancel button has to be visible.
        /// </summary>
        public bool ButtonCancelIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the cancel button.
        /// </summary>
        public RouteViewModel ButtonCancelRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the cancel button.
        /// </summary>
        public string ButtonCancelText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the history button has to be visible.
        /// </summary>
        public bool ButtonHistoryIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the history button.
        /// </summary>
        public RouteViewModel ButtonHistoryRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the history button.
        /// </summary>
        public string ButtonHistoryText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the new button has to be visible.
        /// </summary>
        public bool ButtonNewIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the new button.
        /// </summary>
        public RouteViewModel ButtonNewRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the new button.
        /// </summary>
        public string ButtonNewText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the save and exit button has to be visible.
        /// </summary>
        public bool ButtonSaveAndExitIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the save and exit button.
        /// </summary>
        public RouteViewModel ButtonSaveAndExitRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the save and exit button.
        /// </summary>
        public string ButtonSaveAndExitText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the save and new button has to be visible.
        /// </summary>
        public bool ButtonSaveAndNewIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the save and new button.
        /// </summary>
        public RouteViewModel ButtonSaveAndNewRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the save and new button.
        /// </summary>
        public string ButtonSaveAndNewText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the save button has to be visible.
        /// </summary>
        public bool ButtonSaveIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the save button.
        /// </summary>
        public RouteViewModel ButtonSaveRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the save button.
        /// </summary>
        public string ButtonSaveText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the view button has to be visible.
        /// </summary>
        public bool ButtonViewIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the view button.
        /// </summary>
        public RouteViewModel ButtonViewRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the view button.
        /// </summary>
        public string ButtonViewText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the reset button has to be visible.
        /// </summary>
        public bool ButtonResetIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the route values of the reset button.
        /// </summary>
        public RouteViewModel ButtonResetRoute { get; set; }

        /// <summary>
        /// Gets or sets the text of the rest button.
        /// </summary>
        public string ButtonResetText { get; set; }
    }
}
