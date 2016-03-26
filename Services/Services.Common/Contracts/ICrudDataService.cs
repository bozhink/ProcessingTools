namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;

    public interface ICrudDataService<TServiceModel>
    {
        IQueryable<TServiceModel> All();

        IQueryable<TServiceModel> Get(int skip, int take);

        IQueryable<TServiceModel> Get(object id);

        void Add(TServiceModel model);

        void Update(TServiceModel model);

        void Delete(TServiceModel model);

        void Delete(object id);
    }
}
