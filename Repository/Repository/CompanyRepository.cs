using Contracts.Interfaces;
using Entities.Models;
using Entities.Data;
using System.Linq;
using System.Collections.Generic;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
    FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToList();

}