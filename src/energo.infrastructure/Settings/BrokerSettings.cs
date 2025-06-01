namespace energo.infrastructure.Settings;

public class BrokerSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public int ConsumPeriodMSec { get; set; } = 500;
}
