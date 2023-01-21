using Microsoft.EntityFrameworkCore;
using TestBtrfsReadonly.Data;

namespace TestBtrfsReadonly;

public static class MigrationApplier
{
    public static void ApplyMigrations(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        var schoolContext = new SchoolContext(contextOptions);
        
        schoolContext.Database.Migrate();
    }
}