using System.Linq;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Xunit;

namespace TestBtrfsReadonlyTests;

public class SqlServerFixture : IAsyncLifetime
{
    private const string Database = "db";
    private const string Username = "user";
    private const string Password = "Passw@rd";
    
    private readonly MsSqlTestcontainer _container;
    
    public SqlServerFixture()
    {
        _container = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                //here the username and database name is not used because of throws an error.
                //If it needs to be changed, it should be configured later.
                Password = Password,
            })
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
    }
    public string ConnectionString => string.Concat(_container.ConnectionString.Split(';').First(), $";Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True;");

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await _container.ExecScriptAsync($"CREATE DATABASE {Database}");
        await _container.ExecScriptAsync($"CREATE LOGIN [{Username}] WITH PASSWORD = '{Password}', DEFAULT_DATABASE = {Database}, CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF");
        await _container.ExecScriptAsync($"ALTER SERVER ROLE [sysadmin] ADD MEMBER [{Username}]");
    }
    
    public async Task DisposeAsync() => await _container.StopAsync();
}

[CollectionDefinition("SqlServerCollection")]
public class SqlServerCollection : ICollectionFixture<SqlServerFixture>
{
}