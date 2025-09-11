using System.Collections.Generic;
using Entities.Models;
namespace Contracts.Interfaces;


    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }


    public interface IEmployeeRepository
    {
    }

