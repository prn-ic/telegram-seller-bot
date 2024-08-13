using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;
using TelegramSellerBot.Persistense.Seeds;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class TelegramBotConfiguration : BaseEntityConfiguration<TelegramBot, Guid>
    {
        public override void Configure(EntityTypeBuilder<TelegramBot> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(300);
            builder.Property(t => t.TelegramBotLink).IsRequired();
        }
    }
}
