using energo.domain.Models;
using Microsoft.EntityFrameworkCore;

namespace energo.data.Persistency;

public class ApplicationDbContext : DbContext
{
    private readonly DbContextOptions<ApplicationDbContext> _dboptions;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        _dboptions = options;
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Product> Products => Set<Product>();
}
