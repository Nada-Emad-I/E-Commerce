namespace DomainLayer.Models.ProductModules
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
