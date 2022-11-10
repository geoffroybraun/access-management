namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record ObjectId
{
    private readonly string value;

    private ObjectId(string value)
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

    public static implicit operator ObjectId(string value)
    {
        return new(value);
    }

    public static implicit operator string(ObjectId objectType)
    {
        return objectType.value;
    }
}