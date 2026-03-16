using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcScrapingPlatform.Models;

public class ScrapingLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public int Status { get; set; } // 0: 待機中, 1: 実行中, 2: 完了, 3: エラー

    public string? Message { get; set; }

    [Required]
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? FinishedAt { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
