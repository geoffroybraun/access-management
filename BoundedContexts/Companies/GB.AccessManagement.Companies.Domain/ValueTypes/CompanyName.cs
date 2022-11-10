namespace GB.AccessManagement.Companies.Domain.ValueTypes;

public sealed record CompanyName
{
    private readonly string value;

    private CompanyName(string value)
    {
        value = value.Trim();

        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.value = value;
    }

    public static implicit operator CompanyName(string value)
    {
        return new(value);
    }

    public static implicit operator string(CompanyName companyName)
    {
        return companyName.value;
    }
}