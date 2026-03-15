using Microsoft.AspNetCore.Identity;

namespace DcScrapingPlatform.Models;

public class ApplicationUser : IdentityUser
{
    // 追加のユーザープロフィール情報があればここに記述
    public virtual Profile? Profile { get; set; }
}
