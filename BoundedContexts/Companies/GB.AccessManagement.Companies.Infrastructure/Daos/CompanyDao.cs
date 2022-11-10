using System.ComponentModel.DataAnnotations.Schema;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Companies.Infrastructure.Daos;

[Table("Companies")]
public sealed record CompanyDao : ICompanyMemo
{
    [Column("index")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Index { get; set; }
    
    [Column("id")]
    public Guid Id { get; set; }

    CompanyId ICompanyMemo.Id
    {
        get => this.Id;
        set => this.Id = value;
    }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    CompanyName ICompanyMemo.Name
    {
        get => this.Name;
        set => this.Name = value;
    }

    [NotMapped]
    public IList<Guid> Members { get; set; } = new List<Guid>();

    ICollection<UserId> ICompanyMemo.Members
    {
        get => this.Members.Select(member => (UserId)member).ToList();
        set => this.Members = value.Select(member => (Guid)member).ToList();
    }

    EMemoState IAggregateMemo.State { get; set; }
}