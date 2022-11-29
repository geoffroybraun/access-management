using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Contracts.Commands;

public sealed record DeleteUserAccessCommand(string UserId, string ObjectType, string ObjectId, string Relation) : ICommand
{
}