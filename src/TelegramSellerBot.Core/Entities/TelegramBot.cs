using TelegramSellerBot.Core.Exceptions;

namespace TelegramSellerBot.Core.Entities
{
    public class TelegramBot : BaseEntity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string TelegramBotLink { get; private set; }
        public ICollection<TelegramBotDurationAvailability> Availabilities { get; private set; } =
            new List<TelegramBotDurationAvailability>();
        public TelegramBot() { }
        public TelegramBot(string name, string description, string telegramBotLink)
        {
            Id = Guid.NewGuid();
            ExceptionExtension.ThrowIfStringRangeIsInvalid(name, 3, 100);
            Name = name;
            ExceptionExtension.ThrowIfStringRangeIsInvalid(description, 3, 300);
            Description = description;
            ExceptionExtension.ThrowIsNullOrEmpty(telegramBotLink);
            TelegramBotLink = telegramBotLink;
        }

        public TelegramBot(
            string name,
            string description,
            string telegramBotLink,
            ICollection<TelegramBotDurationAvailability> availabilities
        )
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(name, 3, 100);
            Name = name;
            ExceptionExtension.ThrowIfStringRangeIsInvalid(description, 3, 300);
            Description = description;
            ExceptionExtension.ThrowIsNullOrEmpty(telegramBotLink);
            TelegramBotLink = telegramBotLink;
            Availabilities = availabilities;
        }

        public void SetName(string name)
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(name, 3, 100);
            Name = name;
        }

        public void SetDescription(string description)
        {
            ExceptionExtension.ThrowIfStringRangeIsInvalid(description, 3, 300);
            Description = description;
        }

        public void SetTelegramBotLink(string telegramBotLink)
        {
            ExceptionExtension.ThrowIsNullOrEmpty(telegramBotLink);
            TelegramBotLink = telegramBotLink;
        }
    }
}
