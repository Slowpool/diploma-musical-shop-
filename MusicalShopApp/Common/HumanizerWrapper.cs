using Humanizer;

namespace MusicalShopApp.Common;

public class HumanizerWrapper
{
    public static string? Humanize(DateTime? obj)
    {
        return obj?.Humanize();
    }
}
