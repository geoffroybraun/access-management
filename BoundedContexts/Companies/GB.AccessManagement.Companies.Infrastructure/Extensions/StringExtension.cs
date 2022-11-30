using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Infrastructure.Extensions;

internal static class StringExtension
{
    public static IEnumerable<string> ExcludeNonUsers(this IEnumerable<string> values)
    {
        return values.Where(value => !value.Contains(':'));
    }

    public static IEnumerable<UserId> AsUserIds(this IEnumerable<string> values)
    {
        return values.Select(value => (UserId)value);
    }
}