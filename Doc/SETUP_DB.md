# データベースセットアップ手順 - DcScrapingPlatform

本プロジェクトでは、**Neon (PostgreSQL)** をデータベースとして使用し、**Entity Framework Core (EF Core)** を用いてスキーマ管理を行っています。以下に、データベースを新規作成・再現するための手順を記載します。

## 1. 事前準備
以下のツールがインストールされていることを確認してください。
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- `dotnet-ef` ツール（プロジェクトローカルにインストール済みの場合は `dotnet tool run dotnet-ef` を使用）

## 2. 接続文字列の設定
`Source/DcScrapingPlatform/appsettings.json` の `ConnectionStrings` セクションに Neon DB の接続文字列を設定します。

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=your-host.neon.tech;Database=neondb;Username=your-user;Password=your-password;SSL Mode=Require;Trust Server Certificate=true"
}
```

> [!IMPORTANT]
> パスワード等の機密情報は、本番環境では環境変数や Azure Key Vault 等を使用して管理することを推奨します。

## 3. データベースの構築（マイグレーションの適用）
プロジェクトディレクトリ（`Source/DcScrapingPlatform`）で以下のコマンドを実行し、データベースにテーブルを作成します。

```powershell
# 依存関係の復元
dotnet restore

# データベースへマイグレーションを適用
dotnet tool run dotnet-ef database update
```

これにより、以下のテーブルが作成されます：
- `AspNetUsers` / `AspNetRoles` 等（Identity 認証に関連する標準テーブル）
- `Profiles`（ユーザー固有設定）
- `DcCredentials`（暗号化されたログイン情報）
- `AssetHistories`（資産履歴データ）

## 4. SQL スクリプトによる構築（代替手段）
EF Core ツールが利用できない、または直接 SQL を実行したい場合は、以下のディレクトリにある SQL ファイルを順に実行してください。

- [SQL クエリディレクトリ (Query/)](file:///c:/.Gemini/project/DcScrapingPlatform/Query/)

各テーブル作成クエリ（`01_Create_AspNetRoles.sql` 等）およびインデックス作成クエリが個別に保存されています。

## 5. スキーマの変更（開発者向け）
モデル（`Models/` 配下）を修正した場合は、以下の手順でマイグレーションを追加・更新します。

```powershell
# マイグレーションの追加
dotnet tool run dotnet-ef migrations add [MigrationName]

# DBへの反映
dotnet tool run dotnet-ef database update
```

## 5. 暗号鍵の設定
UFJ ログイン情報の暗号化に必要な鍵を環境変数に設定してください。
- `MASTER_ENCRYPTION_KEY`: 32バイトの Base64 文字列（AES-256用）
