using Microsoft.EntityFrameworkCore;
using lookbook_dotnet_api.data;
using lookbook_dotnet_api.models;
using Microsoft.Extensions.DependencyInjection;
using lookbook_dotnet_api.Repositories;

var builder = WebApplication.CreateBuilder(args);



// Configuração do DbContext para Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Adiciona os serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddAuthorization(); // Registra o serviço de autorização
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// População inicial de dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DataSeeder.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
