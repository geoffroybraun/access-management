using GB.AccessManagement.Core.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class ComputedExtension
{
    public static async Task<UserId[]> Accept(this List<Computed> computedList, IUserSetTreeVisitor visitor)
    {
        List<UserId> result = new();

        foreach (var computed in computedList)
        {
            result.AddRange(await visitor.Visit(computed));
        }

        return result.ToArray();
    }
    
    public static async Task<UserId[]> Accept(this Computed computed, IUserSetTreeVisitor visitor)
    {
        return await visitor.Visit(computed.Userset);
    }
}