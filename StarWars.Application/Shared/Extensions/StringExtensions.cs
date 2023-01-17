using System.Text.RegularExpressions;

namespace StarWars.Application.Shared.Extensions
{
    public static class StringExtensions
    {
        public static long GetRouteParamValue(this string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;
            var id = Regex.Matches(segments.Last(), @"\d+").OfType<Match>().FirstOrDefault().Value;

            long.TryParse(id, out var value);

            return value;
        }
    }
}
