using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.TelegramBot.Contracts;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class ManageBotQuery : ICallbackQueryProcessorContract
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITelegramBotClient _bot;

        public ManageBotQuery(ISubscriptionService subscriptionService, ITelegramBotClient bot)
        {
            _subscriptionService = subscriptionService;
            _bot = bot;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid subscriptionId = Guid.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);
            SubscriptionDto subscription = await _subscriptionService.GetAsync(subscriptionId);

            string message = string.Format(
                """
                <b>Имя</b>: {0}
                <b>Описание</b>: {1}
                <b>Ссылка на канал</b>: {2}
                <b>Статус</b>: {3}
                <b>Дата начала подписки</b>: {4}
                <b>Дата окончания подписки</b>: {5}
                <b>Количество дней</b>: {6}
                """,
                subscription.Service!.Name,
                subscription.Service.Description,
                subscription.Service.TelegramBotLink,
                subscription.Status!.Name,
                subscription.CreatedAt,
                subscription.GetExpirationDate(),
                subscription.GetRemainingDays()
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                message,
                parseMode: ParseMode.Html
            );
        }
    }
}
