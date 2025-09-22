using AutoMapper;
using Contracts.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;

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

    [HttpGet("{id}", Name = "CompanyById")]
        
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

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDto  companyDto)
    {
        if (companyDto == null)
        {
            _logger.LogError("Company is null");
            return BadRequest("Company object is null");

        }
        var companyEntity = _mapper.Map<Company>(companyDto);
        
        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();
        var companyToReturn = _mapper.Map<CompanyDTO>(companyEntity);

        _logger.LogInfo("Company created successfully");
        
        return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
    {
        if (ids == null)
        {
            _logger.LogError("Parameter ids is null");
            return BadRequest("Parameter ids is null");
        }

        var companyEntities = _repository.Company.GetByIds(ids, false);
        if (ids.Count() != companyEntities.Count())
        {
            _logger.LogError("Some ids are not valid in a collection");
            return NotFound();
        }
        ;
        var companyToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        return Ok(companyToReturn);
        
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection == null)
        {
            _logger.LogError("Company collection is null");
            return BadRequest("Company collection is null");
        }
        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach (var VARIABLE in companyEntities)
        {
            _repository.Company.CreateCompany(VARIABLE);
        }
        _repository.Save();

        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        var ids = string.Join(",", companyEntities.Select(c => c.Id));
        return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);
    }
}