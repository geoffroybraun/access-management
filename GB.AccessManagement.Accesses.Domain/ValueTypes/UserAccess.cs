namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record UserAccess(string UserId, string ObjectType, string ObjectId, string Relation);