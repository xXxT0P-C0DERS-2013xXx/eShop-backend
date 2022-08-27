using Catalog.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

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

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();