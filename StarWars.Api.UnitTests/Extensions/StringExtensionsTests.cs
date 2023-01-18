using StarWars.Application.Shared.Extensions;
using Xunit;

namespace StarWars.Api.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("https://swapi.dev/api/films/1/", 1)]
        [InlineData("https://swapi.dev/api/films/2/", 2)]
        [InlineData("https://swapi.dev/api/films/3/", 3)]
        public void StringExtensions_ShouldBeGetAIntPamareterInUrl(string url, long expexted)
        {
            var result = url.GetRouteParamValue();

            Assert.Equal(expexted, result);
        }
    }
}
