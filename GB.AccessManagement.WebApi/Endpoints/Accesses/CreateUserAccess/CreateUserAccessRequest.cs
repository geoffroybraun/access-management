using GB.AccessManagement.Accesses.Contracts.Commands;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.CreateUserAccess;

public sealed record CreateUserAccessRequest(string ObjectType, string ObjectId, string Relation)
{
    public CreateUserAccessCommand ToCommand(Guid userId)
    {
        return new(userId, this.ObjectType, this.ObjectId, this.Relation);
    }
}