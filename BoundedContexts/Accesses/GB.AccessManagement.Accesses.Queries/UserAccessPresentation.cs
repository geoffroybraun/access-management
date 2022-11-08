namespace GB.AccessManagement.Accesses.Queries;

[Serializable]
public sealed record UserAccessPresentation(Guid UserId, string ObjectType, string ObjectId, string Relation);