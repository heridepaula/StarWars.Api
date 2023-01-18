using AutoFixture;
using Moq.AutoMock;

namespace StarWars.Api.UnitTests
{
    public abstract class AbstractTests
    {
        public Fixture Fixture { get; }
        public AutoMocker Mocker { get; }

        public AbstractTests()
        {
            Fixture = new Fixture();
            Mocker = new AutoMocker();
        }
    }
}
