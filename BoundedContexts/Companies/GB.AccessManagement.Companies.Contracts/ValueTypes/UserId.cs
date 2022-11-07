namespace GB.AccessManagement.Companies.Contracts.ValueTypes;

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

    public static implicit operator UserId(Guid value)
    {
        return new(value);
    }

    public static implicit operator Guid(UserId userId)
    {
        return userId.value;
    }
}