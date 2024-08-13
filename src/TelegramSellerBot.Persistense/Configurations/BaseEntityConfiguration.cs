using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Persistense.Data
{
    public class BaseEntityConfiguration<T, TId> : IEntityTypeConfiguration<T>
        where T : BaseEntity<TId>
        where TId : struct
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(i => i.Id).IsUnique();
            builder.Property(t => t.Id).IsRequired();
        }
    }
}
