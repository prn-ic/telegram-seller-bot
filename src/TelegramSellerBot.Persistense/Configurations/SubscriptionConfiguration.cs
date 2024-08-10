using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Persistense.Data
{
    public class SubscriptionConfiguration : BaseEntityConfiguration<Subscription, Guid>
    {
        public override void Configure(EntityTypeBuilder<Subscription> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.TelegramUserId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();

            builder.HasOne(x => x.Service).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Duration).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Status).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
