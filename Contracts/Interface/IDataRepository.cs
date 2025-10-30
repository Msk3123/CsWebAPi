using Entities.Models;

namespace Contracts.Interfaces;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    Company GetCompany(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    
    IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);


}

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
    Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);
    void CreateEmployeeForCompany(Guid companyId, Employee employe);
    
    void DeleteEmployee(Employee employee);
}