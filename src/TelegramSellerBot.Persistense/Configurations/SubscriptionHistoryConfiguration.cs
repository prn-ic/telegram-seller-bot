using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class SubscriptionHistoryConfiguration : BaseEntityConfiguration<SubscriptionHistory, Guid>
    {
        public override void Configure(EntityTypeBuilder<SubscriptionHistory> builder)
        {
            base.Configure(builder);
            builder.Property(sh => sh.Cost).IsRequired();

            builder.HasOne(x => x.Subscription).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.TelegramBot).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}