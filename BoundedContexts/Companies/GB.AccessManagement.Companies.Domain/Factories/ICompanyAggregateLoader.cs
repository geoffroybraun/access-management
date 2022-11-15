using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates.Loaders;

namespace GB.AccessManagement.Companies.Domain.Factories;

public interface ICompanyAggregateLoader : IAggregateRootLoader<CompanyAggregate, CompanyId, ICompanyMemo> { }