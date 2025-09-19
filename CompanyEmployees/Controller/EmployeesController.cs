using AutoMapper;
using Contracts.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;


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

    [HttpGet("{employeeId}", Name = "EmployeeById")]
    public IActionResult GetEmployee(Guid companyId, Guid employeeId)
    {
        _logger.LogInfo("Fetching all employees from database");
        var employee = _repository.Employee.GetEmployee(companyId, employeeId, false);
        if (employee == null)
        {
            _logger.LogError($"Employee with id: {employeeId} doesn't exist in the database.");
            return NotFound();
        }

        var employeeDto = _mapper.Map<EmployeeDTO>(employee);
        return Ok(employeeDto);
    }

    [HttpPost]
    public IActionResult CreateEmployee(Guid companyId,[FromBody] EmployeeForCreationDto employeeDto)
    {
        _logger.LogInfo("Creating new employee");
        if (employeeDto == null)
        {
            _logger.LogError("Employee is null");
            return BadRequest("Employee object is null");
        }
        
        var company = _repository.Company.GetCompany(companyId, false);
        if (company == null)
        {
            _logger.LogError($"Company with id: {companyId} doesn't exist in the database.");
            return BadRequest("Company doesn't exist");       
        }
        var employeeEntity = _mapper.Map<Employee>(employeeDto);
        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();
        _logger.LogInfo("Employee created successfully");
        var employeeToReturn = _mapper.Map<EmployeeDTO>(employeeEntity);
        return CreatedAtRoute("EmployeeById", new { companyId, employeeId  = employeeToReturn.Id }, employeeToReturn);
    }

}