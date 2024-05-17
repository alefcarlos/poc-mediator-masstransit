using MassTransit;
using MassTransit.Mediator;
using SampleMasstransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediator(builder =>
{
    builder.AddConsumer<GetHandler>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (IScopedMediator mediator, bool delay) =>
{
    var forecast = await mediator.SendRequest(new Query(delay));
    return forecast;
});

app.Run();
