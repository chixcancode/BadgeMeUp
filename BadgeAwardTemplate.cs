using BadgeMeUp.Models;

namespace BadgeMeUp
{
    public class BadgeAwardTemplate
    {
        public static string GetEmailBody(Badge badge, User from, string comment)
        {
            var bytes = typeof(BadgeAwardTemplate).Assembly.GetManifestResourceStream("BadgeMeUp.BadgeAwardEmail.html");
            if (bytes == null)
            {
                //Console.WriteLine(typeof(BadgeAwardTemplate).Assembly.GetManifestResourceNames());
                return "";
            }
            using var sr = new StreamReader(bytes);
            var template = sr.ReadToEnd();

            if(comment != null && comment.Length > 0)
            {
                comment = "Message from the sender: " + comment;
            }

            template = template.Replace("{BadgeName}", badge.Name);
            template = template.Replace("{BadgeId}", badge.Id.ToString());
            template = template.Replace("{FromUserName}", from.PrincipalName);
            template = template.Replace("{BadgeDescription}", badge.Description);
            template = template.Replace("{AwardComment}", comment);

            return template;
        }

        public static EmailQueue GetEmailQueue(string toEmail, Badge badge, User from, string comment)
        {
            var email = new EmailQueue
            {
                ToEmail = toEmail,
                Subject = string.Format("You just earned a new badge - {0}!", badge.Name),
                Body = GetEmailBody(badge, from, comment)
            };

            return email;
        }
    }
}
