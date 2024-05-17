using MassTransit.Mediator;

namespace SampleMasstransit;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record Query(bool Delay) : Request<WeatherForecast[]>;

public class GetHandler : MediatorRequestHandler<Query, WeatherForecast[]>
{
    private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

    protected override async Task<WeatherForecast[]> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request.Delay)
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)]
        )).ToArray();
    }
}