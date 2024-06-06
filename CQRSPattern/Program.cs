using System.Reflection;
using CQRSPattern.Features.Products.Commands.Create;
using CQRSPattern.Features.Products.Commands.Delete;
using CQRSPattern.Features.Products.Queries.Get;
using CQRSPattern.Features.Products.Queries.List;
using CQRSPattern.Persistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    var product = await mediatr.Send(new GetProductQuery(id));
    if (product == null) return Results.NotFound();
    return Results.Ok(product);
});

app.MapGet("/products", async (ISender mediatr) =>
{
    var products = await mediatr.Send(new ListProductsQuery());
    return Results.Ok(products);
});

app.MapPost("/products", async (CreateProductCommand command, ISender mediatr) =>
{
    var productId = await mediatr.Send(command);
    if (Guid.Empty == productId) return Results.BadRequest();
    return Results.Created($"/products/{productId}", new { id = productId });
});

app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    await mediatr.Send(new DeleteProductCommand(id));
    return Results.NoContent();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
