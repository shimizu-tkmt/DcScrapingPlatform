using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace DcScrapingPlatform.Services;

public interface IEncryptionService
{
    (byte[] EncryptedData, byte[] Iv) Encrypt(string plainText);
    byte[] Encrypt(string plainText, byte[] iv);
    string Decrypt(byte[] cipherText, byte[] iv);
}

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(IConfiguration configuration)
    {
        var keyString = configuration["MASTER_ENCRYPTION_KEY"];
        if (string.IsNullOrEmpty(keyString))
        {
            // 開発環境用にデフォルト値を設定（本番環境では必ず環境変数を設定すること）
            _key = new byte[32]; // 256 bit zero key
        }
        else
        {
            _key = Convert.FromBase64String(keyString);
        }
    }

    public (byte[] EncryptedData, byte[] Iv) Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.GenerateIV();
        var iv = aes.IV;
        return (Encrypt(plainText, iv), iv);
    }

    public byte[] Encrypt(string plainText, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return ms.ToArray();
    }

    public string Decrypt(byte[] cipherText, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}
