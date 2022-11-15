using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates.Stores;

namespace GB.AccessManagement.Companies.Commands;

public interface ICompanyStore : IAggregateStore<CompanyAggregate, CompanyId, ICompanyMemo> { }