namespace GB.AccessManagement.Companies.Domain.ValueTypes;

public sealed record CompanyId
{
    private readonly Guid value;

    private CompanyId(Guid value)
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

    public static implicit operator CompanyId(Guid value)
    {
        return new(value);
    }

    public static implicit operator Guid(CompanyId companyId)
    {
        return companyId.value;
    }

    public static implicit operator CompanyId(string value)
    {
        return new(Guid.Parse(value));
    }

    public static implicit operator string(CompanyId companyId)
    {
        return companyId.value.ToString();
    }
}