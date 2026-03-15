using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcScrapingPlatform.Models;

public class AssetHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalBalance { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ProfitLoss { get; set; }

    [Required]
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public virtual ApplicationUser User { get; set; } = null!;
}
