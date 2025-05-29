using energo.domain.Contracts.Enums;

namespace energo.domain.Models;

public class Product : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public ProductStatus Status { get; set; }

}
