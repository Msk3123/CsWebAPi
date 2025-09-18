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