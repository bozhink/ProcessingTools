namespace ProcessingTools.Data.Common.Expressions
{
    using ProcessingTools.Contracts.Expressions;

    public sealed class UpdateCommand : IUpdateCommand
    {
        public string FieldName { get; set; }

        public string UpdateVerb { get; set; }

        public object Value { get; set; }
    }
}
