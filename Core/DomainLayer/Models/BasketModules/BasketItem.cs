namespace DomainLayer.Models.BasketModules
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PicturalUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity {  get; set; }
    }
}