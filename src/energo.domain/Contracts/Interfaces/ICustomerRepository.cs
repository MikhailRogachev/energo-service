using energo.domain.Models;

namespace energo.domain.Contracts.Interfaces;

public interface ICustomerRepository
{
    Task<bool> DeleteAsync(Guid customerId);
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> GetCustomerAsync(Guid customerId);
    Task<Customer> UpdateAsync(Customer customer);
    Task<IEnumerable<Customer>> SeachAsync();
}
