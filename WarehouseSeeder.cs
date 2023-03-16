using WarehouseAPI.Entities;

namespace WarehouseAPI
{
    public class WarehouseSeeder
    {
        private readonly WarehouseDbContext _dbContext;
        public WarehouseSeeder(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Employees.Any())
                {
                    var employees = GetEmployees();
                    _dbContext.AddRange(employees);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Employee> GetEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Name = "Tomasz",
                    Surname = "Nosal",
                    Email = "nosal@wh.com",
                    DateOfBirth = new DateTime(1993, 9, 29),
                    Address = new Address()
                    {
                        Country = "Polska",
                        City = "Lublin",
                        Street = "Królowej Jadwigi",
                        PostalCode = "20-282",
                        HouseNumber = "121/212"
                    }
                },
                new Employee()
                {
                    Name = "Michał",
                    Surname = "Nowak",
                    Email = "nowak@wh.com",
                    DateOfBirth = new DateTime(1987, 5, 21),
                    Address = new Address()
                    {
                        Country = "Polska",
                        City = "Lublin",
                        Street = "Filaretów",
                        PostalCode = "20-280",
                        HouseNumber = "13"
                    }
                },
                new Employee()
                {
                    Name = "Kazimierz",
                    Surname = "Czerwonka",
                    Email = "czerwonka@wh.com",
                    DateOfBirth = new DateTime(1988, 1, 30),
                    Address = new Address()
                    {
                        Country = "Polska",
                        City = "Lublin",
                        Street = "Zygmunta Augusta",
                        PostalCode = "20-282",
                        HouseNumber = "99"
                    }
                },
                new Employee()
                {
                    Name = "Maciej",
                    Surname = "Soplica",
                    Email = "soplica@wh.com",
                    DateOfBirth = new DateTime(1997, 11, 22),
                    Address = new Address()
                    {
                        Country = "Polska",
                        City = "Lublin",
                        Street = "Różana",
                        PostalCode = "20-278",
                        HouseNumber = "12/14"
                    }
                }
            };
            return employees;
        }
    }
}
