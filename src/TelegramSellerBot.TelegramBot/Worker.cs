using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceProvider _serviceProvider;

    public Worker(
        ILogger<Worker> logger,
        ITelegramBotClient botClient,
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _botClient = botClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var updateHandler = scope.ServiceProvider.GetRequiredService<UpdateHandler>();

                var receiverOptions = new ReceiverOptions() { AllowedUpdates = [] };
                var me = await _botClient.GetMeAsync(stoppingToken);
                _logger.LogInformation("Start receiving updates for {0}", me.Username ?? "Bot");

                await _botClient.ReceiveAsync(
                    updateHandler: updateHandler,
                    receiverOptions: receiverOptions,
                    cancellationToken: stoppingToken
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Polling failed with exception: {Exception}", ex);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
