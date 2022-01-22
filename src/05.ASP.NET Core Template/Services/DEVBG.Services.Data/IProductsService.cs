namespace DEVBG.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> AddSync(string name, decimal price);

        Task<IEnumerable<TModel>> GetAllProducts<TModel>();
    }
}
