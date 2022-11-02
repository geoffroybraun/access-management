using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Commands.CreateUserAccess;

public sealed record CreateUserAccessCommand(string UserId, string ObjectType, string ObjectId, string Relation) : ICommand;