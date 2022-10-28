using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Application.CreateUserAccess;

public sealed record CreateUserAccessCommand(string UserId, string ObjectType, string ObjectId, string Relation) : ICommand;