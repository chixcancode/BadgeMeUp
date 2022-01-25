using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {
        public List<Badge> Badges;
        public string? Alias;

        public MyBadgesModel()
        {
            Badges = new List<Badge>();
            Badges.Add(new Badge
            {
                Name = "Customer Email",
                Criteria = "Friendly<br />Clear recap with next steps",
                BadgeOwnerAlias = "lalovi",
                BadgeType = BadgeType.SoftSkills,
                Description = "Awarded when you master the art of the follow-up email"
            });
        }

        public void OnGet(string? alias)
        {
            if (alias != null)
                this.Alias = alias;
            else
                this.Alias = "jayoung";
        }
    }
}
