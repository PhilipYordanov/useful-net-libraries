namespace DEVBG.Web.ViewModels.Products
{
    using DEVBG.Data.Models;
    using DEVBG.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
