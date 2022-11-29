namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record UserId
{
    private readonly string value;

    private UserId(string value) => this.value = value;

    public override string ToString()
    {
        return this.value.ToString();
    }

    public static implicit operator UserId(string value)
    {
        return new(value);
    }

    public static implicit operator string(UserId userId)
    {
        return userId.value.ToString();
    }
}