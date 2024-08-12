using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.TelegramBot.CallbackQueries;

namespace TelegramSellerBot.TelegramBot.Services
{
    public static class InlineKeyboardBuilder
    {
        public static InlineKeyboardMarkup GenerateServicesPreview(
            this InlineKeyboardMarkup markup,
            IEnumerable<TelegramBotDto> bots,
            Type callbackType
        )
        {
            foreach (var bot in bots)
            {
                markup.AddNewRow().AddButton(bot.Name!, callbackType.Name + "_" + bot.Id);
            }

            return markup;
        }

        public static InlineKeyboardMarkup GenerateServicePreviewWithPagination(
            this InlineKeyboardMarkup markup,
            IEnumerable<TelegramBotDto> bots,
            int skip,
            int take,
            Type callbackType
        )
        {
            markup.GenerateServicesPreview(bots, typeof(ShowServiceQuery));
            markup
                .AddNewRow()
                .AddButton(
                    "Назад",
                    string.Format(callbackType.Name + "_skip={0}_take={1}", skip - take, take)
                )
                .AddButton(
                    "Далее",
                    string.Format(callbackType.Name + "_skip={0}_take={1}", skip + take, take)
                );

            return markup;
        }

        public static InlineKeyboardMarkup GenerateGoBackButton(
            this InlineKeyboardMarkup markup,
            Type callbackType,
            string query
        )
        {
            markup
                .AddNewRow()
                .AddButton("Вернуться назад", string.Format("{0}_{1}", callbackType.Name, query));

            return markup;
        }

        public static InlineKeyboardMarkup GenerateBotDurationAvailabilities(
            this InlineKeyboardMarkup markup,
            IEnumerable<TelegramBotDurationAvailabilityDto> availabilities,
            Type callbackType
        )
        {
            foreach (var availability in availabilities)
            {
                markup
                    .AddNewRow()
                    .AddButton(
                        string.Format(
                            "{0} дней - {1} руб",
                            (int)availability.Duration!.Id / 24,
                            availability.Cost
                        ),
                        string.Format(
                            "{0}_dId={1}_sId={2}",
                            callbackType.Name,
                            availability.Id,
                            availability.TelegramBot!.Id
                        )
                    );
            }

            return markup;
        }

        public static InlineKeyboardMarkup GenerateGoToManage(
            this InlineKeyboardMarkup markup,
            Guid subscriptionId,
            Type callbackType
        )
        {
            markup
                .AddNewRow()
                .AddButton("Управление подпиской на сервис", string.Format("{0}_sId={1}", callbackType.Name, subscriptionId));

            return markup;
        }
    }
}
