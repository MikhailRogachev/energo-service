﻿namespace inventory_consumer.Models;

public class InventoryUpdateRequest
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
