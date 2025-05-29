using Microsoft.EntityFrameworkCore;

namespace inventory_producer.Models;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    public DbSet<InventoryUpdateRequest> InventoryUpdates { get; set; }
}
