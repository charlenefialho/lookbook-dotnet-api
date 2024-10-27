using lookbook_dotnet_api.data;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using lookbook_dotnet_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext para Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar o repositório genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILookbookRepository, LookbookRepository>();
builder.Services.AddSingleton<IPexelsService>(new PexelsService("SbfUmRH9fXDlzPwaLLS6X6nWpZXCPId3gmxMnRwmiUvfBedUdrKDfCPl"));



// Adiciona os serviços ao contêiner.
builder.Services.AddControllers();
/*.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});*/
builder.Services.AddAuthorization(); // Registra o serviço de autorização
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Adicionando descrições para os modelos
    c.EnableAnnotations();

    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Lookbook API",
            Version = "v1",
            Description = "API para gerenciar lookbooks e produtos",
            Contact = new OpenApiContact
            {
                Name = "LeadTtech",
                Email = "leadtech@gmail.com",
                Url = new Uri("https://leadtech.com"),
            },
            License = new OpenApiLicense
            {
                Name = "Licença de Uso",
                Url = new Uri("https://leadtech.com/licenca"),
            },
        }
    );


});

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lookbook API v1");
        c.RoutePrefix = string.Empty; // Define a página inicial do Swagger como a raiz do app
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();