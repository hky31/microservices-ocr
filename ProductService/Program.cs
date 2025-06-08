var builder = WebApplication.CreateBuilder(args);

// Ajout de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();

// Activer Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
