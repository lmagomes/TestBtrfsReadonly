using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestBtrfsReadonly.Data;

public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
{
    public SchoolContext CreateDbContext(string[] args)
    {
        var contextOptions = new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=p@55W0rd;TrustServerCertificate=True;")
            .EnableSensitiveDataLogging()
            .Options;

        return new SchoolContext(contextOptions);
    }
}