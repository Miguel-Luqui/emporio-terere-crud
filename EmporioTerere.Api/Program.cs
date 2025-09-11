var builder = WebApplication.CreateBuilder(args);

// Configura Kestrel para ouvir na porta 80 em todas as interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o Swagger para aparecer sempre, não apenas no Development
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
