using BadgeMeUp.Models;

namespace BadgeMeUp.Pages.Shared;

public class BadgeWidget
{
    public BadgeWidget(Badge badge, bool showLinks = true)
    {
        Badge = badge;
        ShowLinks = showLinks;
    }

    public Badge Badge { get; set; }

    public bool ShowLinks { get; set; } = true;

    public string FriendlyDescription
    { 
        get
        {
            return Badge.Description.Length > 50 ? Badge.Description.Substring(0, 50) + "..." : Badge.Description;
        }
            
    }
}