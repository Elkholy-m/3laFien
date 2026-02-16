using System.Reflection;
using System.Text;
using Entities.Models;
namespace Repository.Extensions;

public static class QueryGenerator
{
    public static string Parse<T>(string orderBy) {
        var orderTerms = orderBy.Trim().Split(",");
        var propertiesInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        StringBuilder query = new();

        foreach (var term  in orderTerms) {
            string propertyName = term.Split(" ")[0];
            if (string.IsNullOrWhiteSpace(term))
                continue;

            var propertyInfo = propertiesInfos.FirstOrDefault(pi => pi.Name
                    .Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            string direction = term.EndsWith("desc") ? "descending" : "ascending";

            //If the term is price we should order by the price after discount not before
            if (typeof(T) == typeof(Place) && propertyName.Equals("Price", StringComparison.InvariantCultureIgnoreCase)) {
                query.Append($"(Price * (1 - DiscountPercentage / 100)) {direction}, ");
                continue;
            }

            if (propertyInfo is null)
                continue;

            query.Append($"{propertyInfo.Name.ToString()} {direction}, ");
        }

        return query.ToString().TrimEnd(',', ' ');
    }
}
