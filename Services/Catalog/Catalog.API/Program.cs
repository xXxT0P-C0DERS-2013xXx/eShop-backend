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
Injector.InjectServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();