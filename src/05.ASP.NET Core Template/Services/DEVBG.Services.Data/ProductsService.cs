namespace DEVBG.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DEVBG.Data.Common.Repositories;
    using DEVBG.Data.Models;
    using DEVBG.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> AddSync(string name, decimal price)
        {
            Product product = new()
            {
                Name = name,
                Price = price,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<IEnumerable<TModel>> GetAllProducts<TModel>()
            => await this.productsRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<TModel>()
            .ToListAsync();
    }
}
