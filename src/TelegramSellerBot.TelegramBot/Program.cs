using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramSellerBot.Persistense;
using TelegramSellerBot.TelegramBot;
using TelegramSellerBot.TelegramBot.CallbackQueries;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();
var builder = Host.CreateApplicationBuilder(args);

builder
    .Services.AddHttpClient("bot")
    .AddTypedClient<ITelegramBotClient>(
        (httpClient, sp) =>
        {
            TelegramBotClientOptions options = new(configuration["Telegram:ApiKey"]!);
            return new TelegramBotClient(options, httpClient);
        }
    );
;
builder.Services.AddScoped<UpdateHandler>();
builder.Services.AddScoped<GetRangeQuery>();
builder.Services.AddScoped<ShowServiceQuery>();
builder.Services.AddScoped<SubscribeQuery>();
builder.Services.AddScoped<AddSubQuery>();
builder.Services.AddScoped<GetHelpQuery>();
builder.Services.AddScoped<ManageBotQuery>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddNpgsqlPersistense(configuration, "TelegramSellerBot.TelegramBot");
builder.Services.AddApplicationLayer();

var host = builder.Build();
host.Run();
