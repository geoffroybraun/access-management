namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record Relation
{
    private readonly string value;

    private Relation(string value)
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

    public static implicit operator Relation(string value)
    {
        return new(value);
    }

    public static implicit operator string(Relation objectType)
    {
        return objectType.value;
    }
}