namespace ProcessingTools.Contracts
{
    public interface IComplexResult<TResult, TError>
    {
        TResult Result { get; set; }

        TError Error { get; set; }
    }
}
