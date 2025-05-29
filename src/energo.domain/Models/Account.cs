using energo.domain.Contracts.Enums;

namespace energo.domain.Models;

public class Account : BaseModel
{
    public AccountType AccountType { get; set; }
}
