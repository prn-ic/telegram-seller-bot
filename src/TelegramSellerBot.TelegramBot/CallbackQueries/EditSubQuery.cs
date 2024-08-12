using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Requests;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class EditSubQuery : ICallbackQueryProcessorContract
    {
        private readonly ISubscriptionService _subscriptionService;

        private readonly ITelegramBotDurationAvailabilityService _telegramBotDurationAvailabilityService;
        private readonly ITelegramBotClient _bot;

        public EditSubQuery(ISubscriptionService subscriptionService, ITelegramBotDurationAvailabilityService telegramBotDurationAvailabilityService, ITelegramBotClient bot)
        {
            _subscriptionService = subscriptionService;
            _telegramBotDurationAvailabilityService = telegramBotDurationAvailabilityService;
            _bot = bot;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid subscriptionId = Guid.Parse(callbackQuery.Data!.Split("_")[2].Split("=")[1]);
            int availabilityId = int.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);
            var availability = await _telegramBotDurationAvailabilityService.GetAsync(
                availabilityId
            );

            UpdateSubscriptionRequest request =
                new() { SubscriptionId = subscriptionId, Duration = availability.Duration!.Id };

            await _subscriptionService.UpdateAsync(request);

            InlineKeyboardMarkup markup = new InlineKeyboardMarkup().GenerateGoToManage(
                subscriptionId,
                typeof(ManageBotQuery)
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                "Успешно",
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
