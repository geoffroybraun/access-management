namespace GB.AccessManagement.Accesses.Contracts.Presentations;

[Serializable]
public sealed record UserAccessPresentation(string UserId, string ObjectType, string ObjectId, string Relation);