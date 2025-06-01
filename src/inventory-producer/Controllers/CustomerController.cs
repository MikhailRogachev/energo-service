using inventory_producer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace inventory_producer.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCustomerAsync([FromBody] CustomerDto customer)
    {


    }
}
