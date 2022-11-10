using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Contracts.Commands;

public sealed record CreateUserAccessCommand(Guid UserId, string ObjectType, string ObjectId, string Relation) : ICommand;