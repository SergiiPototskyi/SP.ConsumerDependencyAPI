using MassTransit;
using SP.ConsumerDependencyAPI;
using SP.ConsumerDependencyAPI.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ConfigurationCache>();
builder.Services.AddSingleton<IConfigurationCache>(provider => provider.GetRequiredService<ConfigurationCache>());
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<ConfigurationConsumer>();

    x.AddConsumer<InquiryConsumer>();

    x.AddConfigureEndpointsCallback((context, name, cfg) =>
    {
        cfg.AddDependency(context.GetRequiredService<ConfigurationCache>());
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        var queueName = context.GetRequiredService<IEndpointNameFormatter>().Consumer<ConfigurationConsumer>();
        cfg.ReceiveEndpoint(queueName, e =>
        {
            e.ConfigureConsumer<ConfigurationConsumer>(context);
        });
        
        cfg.Host("localhost", "/", h =>
        {
            h.Username("user");
            h.Password("user");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = false;

    options.StartTimeout = TimeSpan.FromSeconds(20);
    options.StopTimeout = TimeSpan.FromSeconds(30);

    options.ConsumerStopTimeout = TimeSpan.FromSeconds(10);
});
builder.Services.Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(60));

builder.Services.AddHostedService<RequestConfigurationWorker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
