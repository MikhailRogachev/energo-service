using inventory_consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_consumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(
    ILogger logger
    ) : ControllerBase
{
    [HttpPost]
    public IActionResult ProcessInventoryUpdate([FromBody] InventoryUpdateRequest request)
    {
        logger.LogInformation("Inventory processStarted");

        return Ok("Inventory update processed successfully.");
    }

}
