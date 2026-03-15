# データベースセットアップガイド (Neon & Local)

本プロジェクトでは、本番環境の安全性を守るため、ローカル開発時には個別のデータベース（Neon の開発用ブランチ）を使用し、機密情報は .NET の **User Secrets** 機能で管理します。

## 1. 接続先の準備 (Neon)

1.  [Neon コンソール](https://console.neon.tech/)にログインします。
2.  プロジェクトの「Branches」から、自分専用のブランチ（例: `dev-user-name`）を作成します。
3.  作成したブランチの「Connection String」をコピーします。

## 2. ローカル環境の設定 (User Secrets)

ソースコード内の `appsettings.json` にはパスワードを記述しないでください。代わりに以下のコマンドをターミナルで実行して、ローカルマシンにのみ保存されるシークレットを設定します。

```powershell
# プロジェクトディレクトリ (Source/DcScrapingPlatform) で実行
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "コピーした接続文字列"
```

## 3. 動作確認

設定が完了したら、以下のコマンドで起動できるか確認してください。

```powershell
dotnet run --project Source/DcScrapingPlatform
```

ログに DB 接続エラーが出なければ設定成功です。

---

> [!IMPORTANT]
> `appsettings.json` の `DefaultConnection` を書き換えてコミットしないでください。
