namespace Catalog.Application.Configuration;

public static class InfrastructureInjector
{
    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = new EndPointCollection { configuration.GetConnectionString("RedisConnection") ?? string.Empty }
            };
            options.InstanceName = "Catalog";
        });
    }
}