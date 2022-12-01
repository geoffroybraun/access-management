using System.ComponentModel.DataAnnotations;

namespace GB.AccessManagement.Accesses.Infrastructure;

public sealed record OpenFgaOptions
{
    private Uri BaseUri => new(this.BaseAddress);
    
    public bool IgnoreInitialization { get; init; }
    
    [Required]
    [Url]
    public string BaseAddress { get; init; } = string.Empty;
    
    [Required]
    public string StoreName { get; init; } = string.Empty;
    
    internal string? StoreId { get; set; }

    internal string Host => this.BaseUri.Authority;

    internal string Scheme => this.BaseUri.Scheme;

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(this.StoreId);
    }
}