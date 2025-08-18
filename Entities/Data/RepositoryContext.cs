using Microsoft.EntityFrameworkCore;
using Entities.Models;
namespace Entities.Data;

public class RepositoryContext:DbContext{
    
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }

    
}