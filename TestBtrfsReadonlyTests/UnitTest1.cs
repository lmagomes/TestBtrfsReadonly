using Microsoft.EntityFrameworkCore;
using TestBtrfsReadonly;
using TestBtrfsReadonly.Data;
using Xunit;

namespace TestBtrfsReadonlyTests;

[Collection("SqlServerCollection")]
public class UnitTest1
{
    private readonly SqlServerFixture _fixture;
    
    public UnitTest1(SqlServerFixture fixture)
    {
        _fixture = fixture;
    }
    
    
    [Fact]
    public void Test1()
    {
        
        using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);
        
        
        for (int i = 0; i < 10; i++)
        {
            context.Database.EnsureDeleted();    
            MigrationApplier.ApplyMigrations(_fixture.ConnectionString);
        }
    }
    
    [Fact]
    public void Test2()
    {
        
        using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);
        
        
        for (int i = 0; i < 10; i++)
        {
            context.Database.EnsureDeleted();    
            MigrationApplier.ApplyMigrations(_fixture.ConnectionString);
        }
    }
    
    [Fact]
    public void Test3()
    {
        
        using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);
        
        
        for (int i = 0; i < 10; i++)
        {
            context.Database.EnsureDeleted();    
            MigrationApplier.ApplyMigrations(_fixture.ConnectionString);
        }
    }
}