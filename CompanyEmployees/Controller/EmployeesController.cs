using AutoMapper;
using Contracts.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controller;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetEmployees(Guid companyId)
    {
        _logger.LogInfo("Fetching all employees from database");

        var company = _repository.Company.GetCompany(companyId, false);
        if (company == null)
        {
            _logger.LogError($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
        }

        var employees = _repository.Employee.GetEmployees(companyId, false);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

        _logger.LogInfo("Все вийшло");

        return Ok(employeesDto);
    }
}