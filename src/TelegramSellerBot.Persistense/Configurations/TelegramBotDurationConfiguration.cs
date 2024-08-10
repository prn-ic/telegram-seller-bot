using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class TelegramBotDurationConfiguration : BaseEntityConfiguration<TelegramBotDuration, TelegramServiceDurations>
    {
        public override void Configure(EntityTypeBuilder<TelegramBotDuration> builder)
        {
            base.Configure(builder);
            
            builder.Property(t => t.Id).HasConversion<int>();

            builder.HasData(
                Enum.GetValues(typeof(TelegramServiceDurations))
                    .Cast<TelegramServiceDurations>()
                    .Select(t => new TelegramBotDuration()
                    {
                        Id = t,
                        Name = t.ToString().ToLower()
                    })
            );
        }
    }
}