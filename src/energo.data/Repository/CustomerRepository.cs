using energo.domain.Contracts.Interfaces;
using energo.domain.Models;

namespace energo.data.Repository;

public class CustomerRepository : ICustomerRepository
{
    public Task<Customer> AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetCustomerAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> SeachAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
