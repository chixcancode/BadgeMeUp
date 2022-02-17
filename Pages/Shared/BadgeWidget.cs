using BadgeMeUp.Models;

namespace BadgeMeUp.Pages.Shared
{
    public class BadgeWidget
    {
        public Badge Badge { get; set; }
        public bool ShowLinks { get; set; } = true;

        public BadgeWidget(Badge badge, bool showLinks = true)
        {
            Badge = badge;
            ShowLinks = showLinks;
        }
    }
}
