namespace DomainLayer.Models
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
