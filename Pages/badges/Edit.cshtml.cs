using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace BadgeMeUp.Pages.Badges;

public class EditModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    private readonly BadgeImageDb _badgeImageDb;

    public EditModel(BadgeDb badgeDb, BadgeImageDb badgeImageDb)
    {
        _badgeDb = badgeDb;
        _badgeImageDb = badgeImageDb;
    }

    [BindProperty]
    public Badge? Badge { get; set; }

    public List<AssignedBadge> BadgeHistory { get; set; }

    [BindProperty]
    public IFormFile? badgeImage { get; set; }

    [BindProperty]
    public List<BadgeType>? BadgeTypes { get; set; }

    public int SelectedBadgeTypeId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        await Populate(id.Value);

        if(Badge == null)
        {
            return NotFound();
        }

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int badgeTypeId, IFormFile? badgeImage)
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }

        //Grab a copy so that we don't have to update every field
        var updateBadge = await _badgeDb.GetBadge(Badge.Id);
        updateBadge.Name = Badge.Name;
        updateBadge.Description = Badge.Description;
        updateBadge.Criteria = Badge.Criteria ?? "";

        if(badgeImage != null)
        {
            updateBadge.BannerImageFileName = badgeImage.FileName;
            var ms = new MemoryStream();
            await badgeImage.CopyToAsync(ms);

            var resized = ResizeImage(ms);

            updateBadge.BadgeStorageUrl = _badgeImageDb.GetBadgeImageUrl(Badge.Id);
            await _badgeImageDb.SaveBadgeImage(Badge.Id, resized, "image/jpeg");
        }

        var selectedBadgeType = await _badgeDb.GetBadgeType(badgeTypeId);
        updateBadge.BadgeType = selectedBadgeType;
        await _badgeDb.UpdateBadge(updateBadge);

        return RedirectToPage("./All");
    }

    private async Task Populate(int id)
    {
        Badge = await _badgeDb.GetBadge(id);
        BadgeTypes = await _badgeDb.GetAllBadgeTypes();
        SelectedBadgeTypeId = Badge.BadgeType.Id;

        if(Badge != null)
        {
            BadgeHistory = await _badgeDb.GetAssignmentHistory(Badge);
        }
    }

    private MemoryStream ResizeImage(MemoryStream ms)
    {
        ms.Seek(0, SeekOrigin.Begin);
        var image = Image.Load(ms);

        var resizeOptions = new ResizeOptions();
        resizeOptions.Size = new Size(250, 100);

        image.Mutate(x => x.Resize(resizeOptions));

        var outStream = new MemoryStream();
        image.Save(outStream, new JpegEncoder());

        return outStream;
    }
}