using InventoryService.Infrastructure.Extensions;
using InventoryService.Infrastructure.Persistence;
using InventoryService.Repositories.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// register EF Core / infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);

// Register MediatR core and handlers via Scrutor (scan the Services assembly)
builder.Services.AddSingleton<IMediator, Mediator>();

var app = builder.Build();

// ensure DB is created/migrated and seed initial data
await DataSeeder.SeedAsync(app.Services);

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
