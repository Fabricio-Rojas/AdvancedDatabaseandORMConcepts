namespace AdvancedDatabaseandORMConcepts.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name 
        { 
            get => _name; 
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    _name = value;
                }
            }
        }
        private string _name;
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
