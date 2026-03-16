using Microsoft.EntityFrameworkCore;
using DcScrapingPlatform.Data;
using DcScrapingPlatform.Models;

namespace DcScrapingPlatform.Services;

public interface IScrapingService
{
    Task RunScrapingAsync(string userId);
}

public class ScrapingService : IScrapingService
{
    private readonly ApplicationDbContext _context;
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger<ScrapingService> _logger;

    public ScrapingService(
        ApplicationDbContext context,
        IEncryptionService encryptionService,
        ILogger<ScrapingService> logger)
    {
        _context = context;
        _encryptionService = encryptionService;
        _logger = logger;
    }

    public async Task RunScrapingAsync(string userId)
    {
        var log = new ScrapingLog
        {
            UserId = userId,
            Status = 1, // 実行中
            StartedAt = DateTime.UtcNow
        };
        _context.ScrapingLogs.Add(log);
        await _context.SaveChangesAsync();

        try
        {
            // TODO: Playwright によるスクレイピング実装
            // ここではプロトタイプとして、ログの更新のみを行う
            _logger.LogInformation("Scraping started for user: {UserId}", userId);
            
            await Task.Delay(2000); // 擬似処理

            log.Status = 2; // 完了
            log.FinishedAt = DateTime.UtcNow;
            log.Message = "Scraping completed successfully (Prototype)";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Scraping failed for user: {UserId}", userId);
            log.Status = 3; // エラー
            log.FinishedAt = DateTime.UtcNow;
            log.Message = $"Error: {ex.Message}";
        }
        finally
        {
            await _context.SaveChangesAsync();
        }
    }
}
