using AutoMapper;
using Contracts.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controller;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }


    [HttpGet]
    public IActionResult GetCompanies()
    {
        _logger.LogInfo("Fetching all companies from database");

        var companies = _repository.Company.GetAllCompanies(false);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDTO>>(companies);

        _logger.LogInfo("Все вийшло! Ви получили інформацію про компанію");

        return Ok(companiesDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _repository.Company.GetCompany(id, false);
        if (company == null)
        {
            _logger.LogError($"Company with id: {id} doesn't exist in the database.");
            return NotFound();
        }

        var companyDto = _mapper.Map<CompanyDTO>(company);
        return Ok(companyDto);
    }
}