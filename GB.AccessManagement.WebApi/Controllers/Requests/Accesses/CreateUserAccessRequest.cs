using GB.AccessManagement.Accesses.Contracts.Commands;

namespace GB.AccessManagement.WebApi.Controllers.Requests.Accesses;

public sealed record CreateUserAccessRequest(string ObjectType, string ObjectId, string Relation)
{
    public CreateUserAccessCommand ToCommand(string userId)
    {
        return new(userId, this.ObjectType, this.ObjectId, this.Relation);
    }
}