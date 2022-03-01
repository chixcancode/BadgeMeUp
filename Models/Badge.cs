namespace BadgeMeUp.Models;

public class Badge
{
    public string? BadgeStorageUrl { get; set; }

    public BadgeType? BadgeType { get; set; }

    public byte[]? BannerImageBytes { get; set; }

    public string? BannerImageContentType { get; set; }

    public string? BannerImageFileName { get; set; }

    public string? Criteria { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Id { get; init; }

    public string Name { get; set; } = string.Empty;
}