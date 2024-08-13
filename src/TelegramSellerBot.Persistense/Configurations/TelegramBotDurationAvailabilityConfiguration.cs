using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;
using TelegramSellerBot.Persistense.Seeds;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class TelegramBotDurationAvailabilityConfiguration
        : BaseEntityConfiguration<TelegramBotDurationAvailability, int>
    {
        public override void Configure(EntityTypeBuilder<TelegramBotDurationAvailability> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().HasIdentityOptions(startValue: 1);
            ;
            builder.Property(t => t.Cost).IsRequired();

            builder.HasOne(t => t.Duration).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(t => t.TelegramBot)
                .WithMany(x => x.Availabilities)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
