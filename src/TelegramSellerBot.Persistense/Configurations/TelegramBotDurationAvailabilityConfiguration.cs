using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class TelegramBotDurationAvailabilityConfiguration : BaseEntityConfiguration<TelegramBotDurationAvailability, int>
    {
        public override void Configure(EntityTypeBuilder<TelegramBotDurationAvailability> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Cost).IsRequired();

            builder.HasOne(t => t.Duration).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Service).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}