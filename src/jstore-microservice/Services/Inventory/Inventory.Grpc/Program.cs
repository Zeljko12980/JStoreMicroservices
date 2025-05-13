
using GrpcContracts;

using Marten;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Dodaj gRPC
builder.Services.AddGrpc();

// Konfiguriši Marten
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
    options.AutoCreateSchemaObjects = AutoCreate.All;
});

var app = builder.Build();

app.MapGrpcService<ProductServiceGrpc>();
app.MapGet("/", () => "gRPC ProductService (Marten) je aktivan.");

app.Run();
