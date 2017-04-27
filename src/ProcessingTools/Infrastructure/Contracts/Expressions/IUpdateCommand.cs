namespace ProcessingTools.Contracts.Expressions
{
    public interface IUpdateCommand
    {
        string FieldName { get; }

        string UpdateVerb { get; }

        object Value { get; }
    }
}
