using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace ruby_testhelper.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AutoMockAttribute : AutoDataAttribute
{
    public AutoMockAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}
