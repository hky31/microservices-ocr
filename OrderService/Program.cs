var builder = WebApplication.CreateBuilder(args);

// Ajout de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddHttpClient("ProductService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001"); // adresse de ProductService
});
var app = builder.Build();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

app.UseCors();


app.UseHttpsRedirection();

// Activer Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
