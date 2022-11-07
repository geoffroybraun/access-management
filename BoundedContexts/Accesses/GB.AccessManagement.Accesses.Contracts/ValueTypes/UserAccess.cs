namespace GB.AccessManagement.Accesses.Contracts.ValueTypes;

public sealed record UserAccess(string UserId, string ObjectType, string ObjectId, string Relation);