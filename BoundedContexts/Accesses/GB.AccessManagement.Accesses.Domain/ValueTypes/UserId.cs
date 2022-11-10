namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record UserId
{
    private readonly Guid value;

    private UserId(Guid value) => this.value = value;

    public override string ToString()
    {
        return this.value.ToString();
    }

    public static implicit operator UserId(Guid value)
    {
        return new(value);
    }

    public static implicit operator UserId(string value)
    {
        return new(Guid.Parse(value));
    }

    public static explicit operator Guid(UserId userId)
    {
        return userId.value;
    }

    public static implicit operator string(UserId userId)
    {
        return userId.value.ToString();
    }
}