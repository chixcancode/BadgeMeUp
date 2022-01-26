using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {
        public List<Badge> Badges;
        public string? Alias;
        public User SelectedUser;

        public MyBadgesModel()
        {
            Badges = new List<Badge>();
            Badges.Add(new Badge
            {
                Name = "Customer Email",
                Criteria = "Friendly<br />Clear recap with next steps",
                //BadgeOwner = new User("jayoung"),
                BadgeType = BadgeType.SoftSkills,
                Description = "Awarded when you master the art of the follow-up email"
            });
            this.SelectedUser = new User("UNUSED");
        }

        public void OnGet(string? alias)
        {
            if(alias == null)
            {
                alias = "jayoung";
            }

            using (var db = new BadgeContext())
            {
                
                var user = db.Users?.Include(x => x.AssignedBadges).FirstOrDefault(x => x.Alias == alias);

                Console.WriteLine(user.AssignedBadges.Count());

                if (user != null)
                {
                    this.SelectedUser = user;
                }
            }
        }
    }
}
