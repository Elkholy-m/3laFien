using Entities.Models;
using Shared.DTO;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class PlaceExtensionsQuery
{
    // === Searching from name and description ===
    public static IQueryable<Place> FilterByIds(this IQueryable<Place> query,
            string? searchTerm, IEnumerable<Guid>? ids)
    {
        if (ids != null && searchTerm != null) {
            query = query.Where(place => ids.Contains(place.PlaceId));
        }
        return query;
    }

    // === Filtering By Price Range And Min Rate ===
    public static IQueryable<Place> FilterByRange(this IQueryable<Place> query,
            PlaceQueryString queryString)
    {
        if (queryString.ValidPriceRange) {
            query = query.Where(place =>
                    queryString.MinPrice <= place.Price - (place.Price * place.DiscountPercentage / 100) &&
                    queryString.MaxPrice >= place.Price - (place.Price * place.DiscountPercentage / 100));
        }
        
        if (queryString.ValidMinRate) {
            query = query.Where(place => queryString.MinRate <= place.Rate);
        }

        return query;
    }

    // === Filter The Open Only Places ===
    public static IQueryable<Place> FilterByOpenOnly(this IQueryable<Place> query, bool? openOnly) {
        if (openOnly.HasValue){
            var now = DateTime.UtcNow;
            var dbWeekDay = ((int)now.DayOfWeek + 1) % 7;
            var currentTime = now.TimeOfDay;

            // Filter Opened Places Only
            if (openOnly.Value) {
                query = query.Where(place => place.PlaceSchedules!.Any(schedule =>
                            (int)schedule.WeekDay == dbWeekDay &&
                            currentTime > schedule.OpenTime &&
                            currentTime < schedule.ClosedTime));
            } // Filter Closed Places Only
            else {
                query = query.Where(place => place.PlaceSchedules!.All(schedule =>
                            (int)schedule.WeekDay != dbWeekDay ||
                            currentTime < schedule.OpenTime ||
                            currentTime > schedule.ClosedTime));

            }
        }
        return query;
    }

    // === Filter The Places That Have Discount ===
    public static IQueryable<Place> FilterByDiscount(this IQueryable<Place> query, bool? discountOnly) {
            if (discountOnly.HasValue) {
                if (discountOnly.Value)
                    query = query.Where(place => place.DiscountPercentage != 0);
                else 
                    query = query.Where(place => place.DiscountPercentage == 0);
            }
            return query;
    }

    // === Filter By Category ===
    public static IQueryable<Place> FilterByCategory(this IQueryable<Place> query, int? categoryId) {
        if (categoryId.HasValue) {
            query = query.Where(place => place.CategoryId == categoryId.Value);
        }
        return query;
    }

    // === Filter By Country ===
    public static IQueryable<Place> FilterByCountry(this IQueryable<Place> query, int? countryId) {
        if (countryId != null) {
            query = query.Where(place => place.CountryId == countryId);
        }
        return query;
    }

    // === Filter By State ===
    public static IQueryable<Place> FilterByState(this IQueryable<Place> query, int? stateId) {
        if (stateId != null) {
            query = query.Where(place => place.StateId == stateId);
        }
        return query;
    }

    // === Filter By City ===
    public static IQueryable<Place> FilterByCity(this IQueryable<Place> query, int? cityId) {
        if (cityId != null) {
            query = query.Where(place => place.CityId == cityId);
        }
        return query;
    }

    public static IQueryable<Place> Sort(this IQueryable<Place> query, string? orderBy) {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query.OrderByDescending(x => x.Rate);

        var sortingQuery = QueryGenerator.Parse<Place>(orderBy);

        if (string.IsNullOrWhiteSpace(sortingQuery))
            return query.OrderByDescending(x => x.Rate);
        
        return query.OrderBy(sortingQuery);
    }
}
