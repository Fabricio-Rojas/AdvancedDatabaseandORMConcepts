namespace AdvancedDatabaseandORMConcepts.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public HashSet<Order> Orders { get; set; } = new HashSet<Order>();
        public Product() { }
        public Product(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
            Orders = new HashSet<Order>();
        }
    }
}
