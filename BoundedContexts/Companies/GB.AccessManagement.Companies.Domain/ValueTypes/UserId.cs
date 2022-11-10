namespace GB.AccessManagement.Companies.Domain.ValueTypes;

public sealed record UserId
{
    private readonly Guid value;

    private UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.value = value;
    }

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

    public static implicit operator Guid(UserId userId)
    {
        return userId.value;
    }

    public static implicit operator string(UserId userId)
    {
        return userId.value.ToString();
    }
}