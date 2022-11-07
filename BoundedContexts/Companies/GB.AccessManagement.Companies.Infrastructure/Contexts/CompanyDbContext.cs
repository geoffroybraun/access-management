using GB.AccessManagement.Companies.Infrastructure.Daos;
using GB.AccessManagement.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace GB.AccessManagement.Companies.Infrastructure.Contexts;

public sealed class CompanyDbContext : DbContext, ISelfScopedService
{
    public DbSet<CompanyDao> Companies { get; set; } = default!;

    public CompanyDbContext(DbContextOptions options) : base(options) { }
}