namespace AdvancedDatabaseandORMConcepts.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public bool PrimaryAddress { get; set; }
        public CustomerAddress() { }
        public CustomerAddress(Customer customer, Address address, bool primaryAddress)
        {
            CustomerId = customer.Id;
            Customer = customer;
            AddressId = address.Id;
            Address = address;
            PrimaryAddress = primaryAddress;
        }
    }
}
