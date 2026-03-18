# ウォークスルー - アーキテクチャ設計の修正

## 概要
スクレイピング処理を別プロジェクト（Worker Service）に分離する「案1」の採用に伴い、プロジェクトの基本設計および詳細設計を修正しました。

## 修正内容

### 1. 基本設計の更新 ([基本設計書.md](../../Doc/基本設計書.md))
- **技術スタック**: `DcScrapingPlatform.Worker` (Console App) と `DcScrapingPlatform.Shared` (Class Library) を追加。
- **インフラ構成**: Worker のホスティング先として Azure Container Apps 等を想定。
- **機能一覧**: スクレイピングが Web アプリから非同期で依頼され、Worker で実行される仕組みを明記。

### 2. 詳細設計の更新 ([詳細設計書.md](../../Doc/詳細設計書.md))
- **レイヤー構成**: 以下の 4層構造に整理。
    - `Client`: インタラクティブ UI
    - `Web`: API / SSR / 実行依頼
    - `Worker`: スクレイピング実効
    - `Shared`: DB / モデル / 共通ロジック
- **処理フロー**: DB (`ScrapingLogs`) をキューとして使用する非同期実行フローを定義。

## 証跡
- **GitHub Issue**: #39
- **Pull Request**: [#40](https://github.com/shimizu-tkmt/DcScrapingPlatform/pull/40)

## 次のステップ
1. ユーザーによる PR のマージ。
2. マージ後、`feature/#39-arch-design-update` ブランチの削除。
3. Shared プロジェクトの作成および既存ロジックの移行作業の開始。
