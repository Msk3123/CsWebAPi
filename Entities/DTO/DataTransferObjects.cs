namespace Entities.DTO;

public class CompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FullAddress { get; set; }
}

public class EmployeeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
}

public class CompanyForCreationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
}

public class EmployeeForCreationDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
}