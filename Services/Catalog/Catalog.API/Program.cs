var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(
            new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"] ?? "localhost:9200"))
            {
                IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".","-")}-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1
            })
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

var services = builder.Services;
services.AddControllers();
services.AddApiVersioning();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<CatalogContext>(x =>
    x.UseNpgsql(builder.Configuration["ConnectionString:PostgresConnection"]));
services.AddAutoMapper(Assembly.GetAssembly(typeof(MapperConfiguration)));
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CategoryModelValidator)));

BusinessLogicInjector.InjectServices(services);
InfrastructureInjector.InjectServices(services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();