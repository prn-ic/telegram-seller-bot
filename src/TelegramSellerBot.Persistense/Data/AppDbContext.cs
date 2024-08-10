using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Persistense.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Subscription> Subscriptions => Set<Subscription>();
        public virtual DbSet<SubscriptionHistory> SubscriptionHistories =>
            Set<SubscriptionHistory>();
        public virtual DbSet<SubscriptionStatus> SubscriptionStatuses => Set<SubscriptionStatus>();
        public virtual DbSet<TelegramBot> TelegramBots => Set<TelegramBot>();
        public virtual DbSet<TelegramBotDuration> TelegramBotDurations =>
            Set<TelegramBotDuration>();
        public virtual DbSet<TelegramBotDurationAvailability> TelegramBotDurationAvailabilities =>
            Set<TelegramBotDurationAvailability>();

        public AppDbContext() { }

        public AppDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
