namespace ProcessingTools.Common.Providers.Contracts
{
    public interface IRandomProvider
    {
        int GetRandomNumber(int min, int max);
    }
}