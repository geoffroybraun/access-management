using System.ComponentModel.DataAnnotations.Schema;
using GB.AccessManagement.Companies.Domain.Memos;
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
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    public EMemoState State { get; set; }
}