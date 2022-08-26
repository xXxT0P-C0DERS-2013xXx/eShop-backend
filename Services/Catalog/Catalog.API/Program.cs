var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
services.AddApiVersioning();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<CatalogContext>(x =>
    x.UseNpgsql(builder.Configuration["ConnectionString:PostgresConnection"]));
services.AddAutoMapper(Assembly.GetAssembly(typeof(MapperConfiguration)));
services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = new ConfigurationOptions
    {
        AbortOnConnectFail = false,
        EndPoints = new EndPointCollection { builder.Configuration["ConnectionString:RedisConnection"] }
    };
    options.InstanceName = "Catalog";
});
Injector.InjectServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();