using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Contracts.Commands;

public sealed record CreateObjectAccessCommand(string ParentType, string ParentId, string ObjectType, string ObjectId, string Relation) : ICommand;