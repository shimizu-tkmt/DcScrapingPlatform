using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcScrapingPlatform.Models;

public class Profile
{
    [Key]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    public string? SpreadsheetId { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ApplicationUser User { get; set; } = null!;
}
