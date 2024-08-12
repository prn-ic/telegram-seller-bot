using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class StopSubQuery : ICallbackQueryProcessorContract
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITelegramBotClient _bot;

        public StopSubQuery(ISubscriptionService subscriptionService, ITelegramBotClient bot)
        {
            _subscriptionService = subscriptionService;
            _bot = bot;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid subscriptionId = Guid.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);
            await _subscriptionService.DeclineAsync(subscriptionId);
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

            InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

            if (subscription.Status.Id.Equals(SubscriptionStatuses.Inactive))
                markup.GeneraneInactiveManageMenu(subscriptionId, typeof(PreEditSubQuery));
            else
                markup.GenerateActiveManageMenu(
                    subscriptionId,
                    typeof(StopSubQuery),
                    typeof(PreEditSubQuery)
                );

            markup.GenerateGoBackButton(typeof(GetMyQuery), "");

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                message,
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
