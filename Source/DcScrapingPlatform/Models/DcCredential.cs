using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcScrapingPlatform.Models;

public class DcCredential
{
    [Key]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public byte[] EncryptedId { get; set; } = Array.Empty<byte>();

    [Required]
    public byte[] EncryptedPassword { get; set; } = Array.Empty<byte>();

    [Required]
    public byte[] Iv { get; set; } = Array.Empty<byte>();

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ApplicationUser User { get; set; } = null!;
}
