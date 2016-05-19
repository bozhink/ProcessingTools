namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IValidator
    {
        Task Validate();
    }
}