// <copyright file="ActionIdentifierAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Attributes.Tasks
{
    using System;
    using ProcessingTools.Common.Enumerations.Tasks;

    /// <summary>
    /// Action identifier.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ActionIdentifierAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionIdentifierAttribute"/> class.
        /// </summary>
        /// <param name="actionType">Action type.</param>
        public ActionIdentifierAttribute(ActionType actionType)
        {
            this.ActionType = actionType;
        }

        /// <summary>
        /// Gets the action type.
        /// </summary>
        public ActionType ActionType { get; }
    }
}
