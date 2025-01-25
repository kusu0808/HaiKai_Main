using System.Threading;
using Cysharp.Threading.Tasks;
using IA;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineUpWayYatsuAppearAtLastEscape(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _hasSavedDaughter is true, cancellationToken: ct);
            await UniTask.WaitUntil(() => _borders.ShrineUpWayYatsuAppearAtLastEscape.IsIn(_player.Position) is true, cancellationToken: ct);
            await DoEvent(ct);

            async UniTask DoEvent(CancellationToken ct)
            {
                _player.IsPlayerControlEnabled = false;
                _player.IsVisible = false;
                _isWalkingSoundMuted.Value = true;

                _daughter.SpawnHere(_points.ShrineUpWayDaughterAtLastEscapeSpawnPoint);

                int i = await UniTask.WhenAny(
                    _objects.ShrineWayYatsuComeAtLastEscapeTimeline.PlayOnce(ct),
                    WaitForCutSceneCancel(ct)
                );
                _objects.ShrineWayYatsuComeAtLastEscapeTimeline.StopForcibly();

                _yatsu.SpawnHere(_points.ShrineUpWayYatsuComeAtLastEscapeSpawnPoint);

                _isWalkingSoundMuted.Value = false;
                _player.IsVisible = true;
                _player.IsPlayerControlEnabled = true;
            }

            // 0.5秒待ってから、(カットシーンをキャンセルするための)キャンセル入力を受け付ける
            async UniTask WaitForCutSceneCancel(CancellationToken ct)
            {
                await UniTask.WaitForSeconds(0.5f, cancellationToken: ct);
                await UniTask.WaitUntil(() => InputGetter.Instance.PlayerCancel.Bool, cancellationToken: ct);
            }
        }
    }
}