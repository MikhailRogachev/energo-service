using energo.infrastructure.Settings;
using ruby_testhelper;

namespace energo.management.unit.tests;

public class BrokerSettingsTests
{
    [Fact]
    public void GetOptions()
    {
        var options = TestHelper.GetOptionsFromAppSettings<ConsumingBrokerSettings>();

        Assert.NotNull(options);
    }
}
