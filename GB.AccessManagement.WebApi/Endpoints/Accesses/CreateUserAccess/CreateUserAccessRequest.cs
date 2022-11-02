using GB.AccessManagement.Accesses.Commands.CreateUserAccess;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.CreateUserAccess;

public sealed record CreateUserAccessRequest(string ObjectType, string ObjectId, string Relation)
{
    public CreateUserAccessCommand ToCommand(string userId)
    {
        return new(userId, this.ObjectType, this.ObjectId, this.Relation);
    }
}