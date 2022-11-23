using Microsoft.AspNetCore.Mvc.Testing;

namespace Sample_TeamCity.Integration_Test.Factory;

public class TeamCityWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Production");
    }
}