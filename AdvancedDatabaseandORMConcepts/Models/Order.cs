namespace AdvancedDatabaseandORMConcepts.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _quantity = value;
                }
            }
        }
        private int _quantity;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerAddressId { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public Order() { }
        public Order(int quantity, Product product, CustomerAddress customerAddress)
        {
            Quantity = quantity;
            ProductId = product.Id;
            Product = product;
            CustomerAddressId = customerAddress.Id;
            CustomerAddress = customerAddress;
        }
    }
}
