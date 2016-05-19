namespace ProcessingTools.Contracts.Types
{
    /// <summary>
    /// Represents validation status code returned by a ValidationService.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// Returned result is valid.
        /// </summary>
        Valid = 0,

        /// <summary>
        /// Returned result is not valid.
        /// </summary>
        Invalid = 1,

        /// <summary>
        /// Can not determine if the returned result is valid or invalid.
        /// </summary>
        Undefined = 2
    }
}
