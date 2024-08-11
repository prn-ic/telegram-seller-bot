using TelegramSellerBot.Persistense;
using TelegramSellerBot.TelegramBot;

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddNpgsqlPersistense(configuration, "TelegramSellerBot.TelegramBot");

var host = builder.Build();
host.Run();
