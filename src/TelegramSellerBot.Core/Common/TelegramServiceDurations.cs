namespace TelegramSellerBot.Core.Common
{
    /// <summary>
    /// Перечисление, представляющее собой периоды подписки
    /// где числовые значение - часы,
    /// в качестве месячной даты считается, что в месяце 31 день всегда (расчет на наихудший случай)
    /// </summary>
    public enum TelegramServiceDurations
    {
        Week = 168,
        TwoWeek = 336,
        Month = 744,
        ThreeMonth = 2232,
        HalfYear = 4464,
        Year = 8760
    }
}
