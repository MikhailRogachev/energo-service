using energo.infrastructure.Interfaces;
using inventory_producer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace inventory_producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(
    IProducerService producerService,
    ILogger<InventoryController> logger
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpdateInventory([FromBody] InventoryUpdateRequest request)
    {
        logger.LogInformation("Start Producing message");

        var message = JsonSerializer.Serialize(request);

        logger.LogInformation("Message to produce: {msg}", message);

        await producerService.ProduceAsync("InventoryUpdate", message);

        return Ok("Inventory updated successfully ...");
    }
}
