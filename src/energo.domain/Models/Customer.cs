using energo.domain.Contracts.Enums;

namespace energo.domain.Models;

public class Customer : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; }
    public IReadOnlyCollection<Account>? Accounts { get; set; }
}
