namespace GB.AccessManagement.Accesses.Contracts.ValueTypes;

public sealed record ObjectType
{
    private readonly string value;

    private ObjectType(string value)
    {
        value = value.Trim();

        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.value = value;
    }

    public override string ToString()
    {
        return this.value;
    }

    public static implicit operator ObjectType(string value)
    {
        return new(value);
    }

    public static implicit operator string(ObjectType objectType)
    {
        return objectType.value;
    }
}