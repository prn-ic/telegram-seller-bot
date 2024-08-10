using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class TelegramBotRepository : GeneralRepository<TelegramBot, Guid>, ITelegramBotRepository
    {
        public TelegramBotRepository(AppDbContext context) : base(context)
        {
        }
    }
}