namespace DEVBG.Data.Models
{
    using DEVBG.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
