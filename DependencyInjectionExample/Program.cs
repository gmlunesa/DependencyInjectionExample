var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISingletonExampleService>(new ExampleService());
builder.Services.AddScoped<IScopedExampleService, ExampleService>();
builder.Services.AddTransient<ITransientExampleService, ExampleService>();

var app = builder.Build();

app.MapGet("/example", (ISingletonExampleService exampleSingle1, ISingletonExampleService exampleSingle2,
                        IScopedExampleService exampleScoped1, IScopedExampleService exampleScoped2,
                        ITransientExampleService exampleTransient1, ITransientExampleService exampleTransient2) =>
{
    return $"Singleton instance: {exampleSingle1.Id}\n" +
            $"Singleton instance: {exampleSingle2.Id}\n\n" +
            $"Scoped instance 1: {exampleScoped1.Id}\n" +
            $"Scoped instance 2: {exampleScoped2.Id}\n\n" +
            $"Transient instance 1: {exampleTransient1.Id}\n" +
            $"Transient instance 2: {exampleTransient2.Id}";
});

app.Run();

public interface ISingletonExampleService : IExampleService { }
public interface IScopedExampleService : IExampleService { }
public interface ITransientExampleService : IExampleService { }

public interface IExampleService
{
    Guid Id { get; }
}

public class ExampleService : ISingletonExampleService, IScopedExampleService, ITransientExampleService
{
    public Guid Id { get; } = Guid.NewGuid();
}

