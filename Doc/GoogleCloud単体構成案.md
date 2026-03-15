# Google Cloud 単体での構築案とコスト見積

Vercel や Supabase を使わず、すべての機能を Google Cloud (GCP) に集約した場合の構成案とコスト見積もりです。

---

## 1. 構成案 (GCP Native)

Google Cloud のエコシステム内で完結させるための推奨構成です。

| 役割 | 採用サービス | 内容 |
| :--- | :--- | :--- |
| **Frontend / API** | **Cloud Run** | Next.js をコンテナ化してホスト。 |
| **Authentication** | **Firebase Auth** | Google ログインを簡単に実装。GCPの一部としてシームレスに連携。 |
| **Database** | **Firestore** | もしくは **Cloud SQL**。Firestore は無料枠が広く、Cloud SQL は本格的ですが有料です。 |
| **実行基盤 (Worker)** | **Cloud Run** | （既存案と同じ）Playwright によるスクレイピング。 |
| **Scheduler** | **Cloud Scheduler** | 1日1回の実行トリガー。 |

---

## 2. コスト見積（Google Cloud のみの時）

| コンポーネント | サービス名 | 無料枠内での利用 | 有料化時の目安 |
| :--- | :--- | :--- | :--- |
| **Web / Worker** | Cloud Run | 180,000 vCPU秒まで無料。 | 約50人超で月額 数百円〜 |
| **Auth** | Firebase Auth | **永久無料**（電話認証以外）。 | 0 円 |
| **NoSQL DB** | Firestore | 保存 1GB、1日5万読込まで無料。 | **ほぼ 0 円**（小規模時） |
| **SQL DB** | Cloud SQL | **無料枠なし**。 | **月額 約1,500円〜** |
| **Scheduler** | Cloud Scheduler | 3回/月まで無料。 | 10円程度 |

### 注意点：データベースの選択
- **安さ優先 (Firestore)**: ドキュメント型 DB です。Supabase (SQL) とは使い勝手が異なりますが、**完全無料**で運用しやすいです。
- **本格派 (Cloud SQL)**: Supabase と同様に SQL が使えますが、**月額 約1,500円〜 の固定費**がかかります。

---

## 3. 利点と欠点

### 利点
- **一元管理**: 請求や権限管理（IAM）が Google Cloud 1つにまとまり、非常に管理しやすい。
- **性能**: 同一リージョン内での通信になるため、サービス間のレイテンシが最小限になる。
- **拡張性**: 将来的に独自のドメインや大規模なトラフィックにも柔軟に対応可能。

### 欠点
- **SQL の固定費**: 本格的な SQL データベースを使おうとすると、Vercel/Supabase 構成よりも月額費用が高くなります。
- **設定の難易度**: Vercel と比べると、コンテナのビルドやデプロイ設定に少し専門知識が必要です。

---

## 結論

**「Google Cloud 1つにまとめたい」かつ「無料で維持したい」場合は、データベースに Firestore (NoSQL) を採用するのが正解です。**

もし SQL の操作性を重視しつつ無料で、という場合は、当初の **Supabase 併用案** が最もコストメリットがあります。

Google Cloud 単体（Firestore採用）で進めてよろしいでしょうか？
