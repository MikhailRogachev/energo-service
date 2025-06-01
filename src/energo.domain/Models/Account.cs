using energo.domain.Contracts.Enums;

namespace energo.domain.Models;

public class Account : BaseModel
{
    public Guid? CustomerId { get; set; }
    public AccountType AccountType { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ValidUntil { get; set; }
}
