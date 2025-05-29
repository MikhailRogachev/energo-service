using inventory_producer.Models;
using inventory_producer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace inventory_producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(
    ProducerService producerService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpdateInventory([FromBody] InventoryUpdateRequest request)
    {
        var message = JsonSerializer.Serialize(request);

        await producerService.ProduceAsync("InventoryUpdate", message);

        return Ok("Inventory updated successfully ...");
    }
}
