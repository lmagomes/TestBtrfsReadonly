using System;
using Microsoft.EntityFrameworkCore;
using TestBtrfsReadonly.Data;
using TestBtrfsReadonly.Models;
using Xunit;

namespace TestBtrfsReadonlyTests;

[Collection("SqlServerCollection")]
public class UnitTest2
{
    private readonly SqlServerFixture _fixture;
    
    public UnitTest2(SqlServerFixture fixture)
    {
        _fixture = fixture;
        
        using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    
    [Fact]
    public async void Can_add_data()
    {
        await using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);

        var r = new Random();

        for (var i = 0; i < 100; i++)
        {
            for (var j = 0; j < 1000; j++)
            {
                context.Courses.Add(new Course
                {
                    Credits = r.Next(),
                    Title = $"TITLE {r.Next()}",
                    Department = new Department
                    {
                        Budget = 1.2m,
                        Name = $"NAME {r.Next()}",
                    }
                });
            }
            await context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void Can_delete_data()
    {
        await using var context = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
            .UseSqlServer(_fixture.ConnectionString)
            .Options);
        
        var r = new Random();
        context.Courses.Add(new Course
        {
            Credits = r.Next(),
            Title = $"TITLE {r.Next()}",
            Department = new Department
            {
                Budget = 1.2m,
                Name = $"NAME {r.Next()}",
            }
        });
        
        await context.SaveChangesAsync();
        
        context.Courses.RemoveRange();
    }
    
}