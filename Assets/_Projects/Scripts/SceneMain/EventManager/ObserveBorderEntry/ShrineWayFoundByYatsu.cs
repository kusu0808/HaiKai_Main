using System.Threading;
using Cysharp.Threading.Tasks;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineWayFoundByYatsu(CancellationToken ct)
        {
            _borders.IsFromUnderStageToShrineWayBorderEnabled = true;
            await UniTask.WaitUntil(() => _borders.ShrineWayFoundedEvent.IsIn(_player.Position) is true, cancellationToken: ct);
            await DoEvent(ct);

            async UniTask DoEvent(CancellationToken ct)
            {
                _player.IsPlayerControlEnabled = false;
                _player.IsVisible = false;
                _isWalkingSoundMuted.Value = true;
                _player.IsCameraEaseCut = true;

                await _uiElements.BlackImage.FadeOut(0.5f, ct);
                await UniTask.WaitForSeconds(0.5f, cancellationToken: ct);
                int i = await UniTask.WhenAny(
                    UniTask.WhenAll(
                        _objects.ShrineWayFoundByYatsuTimeline.PlayOnce(ct),
                        WaitForFadeInOnEventBegin(ct),
                        OnPlaying(ct)
                    ),
                    WaitForCutSceneCancel(ct)
                );

                if (i == 1)
                {
                    // キャンセルされた
                    // このとき、タイムライン再生以外のタスクは完了しているはず
                    // フェードアウト → タイムラインの再生処理 → フェードイン を行う
                    await CancelTimeline(ct);
                }

                _yatsu.SpawnHere(_points.ShrineWayYatsuSpawnPoint);

                _player.IsCameraEaseCut = false;
                _isWalkingSoundMuted.Value = false;
                _player.IsVisible = true;
                _player.IsPlayerControlEnabled = true;
            }

            // 1フレーム待ってプレイヤーをテレポートさせ、ゲーム全体のフラグを更新する
            async UniTask OnPlaying(CancellationToken ct)
            {
                await UniTask.NextFrame(cancellationToken: ct);

                _player.SetTransform(_points.ShrineWayPlayerTeleportPoint);

                _objects.ShrineWayRock.IsEnabled = true;
                _borders.IsFromUnderStageToShrineWayBorderEnabled = false;
                _objects.VillageWayCannotGoBackAfterWarehouse.IsEnabled = true;
            }

            // 5フレーム待ってフェードインする（見えるようになる）
            async UniTask WaitForFadeInOnEventBegin(CancellationToken ct)
            {
                await UniTask.DelayFrame(5, cancellationToken: ct);
                await _uiElements.BlackImage.FadeIn(0.5f, ct);
            }

            // 10フレーム待ってから、(カットシーンをキャンセルするための)キャンセル入力を受け付ける
            async UniTask WaitForCutSceneCancel(CancellationToken ct)
            {
                await UniTask.DelayFrame(10, cancellationToken: ct);
                await UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct);
            }

            async UniTask CancelTimeline(CancellationToken ct)
            {
                await _uiElements.BlackImage.FadeOut(0.5f, ct);
                _objects.ShrineWayFoundByYatsuTimeline.StopForcibly();
                await _uiElements.BlackImage.FadeIn(0.5f, ct);
            }
        }
    }
}