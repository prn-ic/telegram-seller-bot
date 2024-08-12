using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Configurations
{
    public class SubscriptionStatusConfiguration
        : BaseEntityConfiguration<SubscriptionStatus, SubscriptionStatuses>
    {
        public override void Configure(EntityTypeBuilder<SubscriptionStatus> builder)
        {
            base.Configure(builder);
            builder.Property(ss => ss.Id).HasConversion<int>();

            builder.HasData(
                Enum.GetValues(typeof(SubscriptionStatuses))
                    .Cast<SubscriptionStatuses>()
                    .Select(ss => new SubscriptionStatus()
                    {
                        Id = ss,
                        Name = ss.ToString().ToLower()
                    })
            );
        }
    }
}
