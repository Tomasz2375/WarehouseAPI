namespace WarehouseAPI.Entities
{
    public abstract class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
    }
    public class EmployeeAddress : Address
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
    public class ClientAddress : Address
    {
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
