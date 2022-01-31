namespace BadgeMeUp.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Criteria { get; set; } = "";
        public string Description { get; set; } = "";

        //nullable avoids a model binding error in mvc
        public BadgeType? BadgeType { get; set; }

        public string? BannerImageFileName { get; set; }
        public byte[]? BannerImageBytes { get; set; }
        public string? BannerImageContentType { get; set; }
    }
}
