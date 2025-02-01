using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid GeneralDaughterSaysCannotGo(CancellationToken ct)
        {
            // 道路2つはレイヤー0、娘を助けた後の脱出時2つはレイヤー1
            var borders = _borders.DaughterSaysCannotGo;

            bool IsIn()
            {
                if (_hasSavedDaughter is false)
                    return borders.IsInAny(_player.Position, 0);
                else
                    return borders.IsInAny(_player.Position, 0, 1);
            }

            while (true)
            {
                await UniTask.WaitUntil(() => IsIn() is false, cancellationToken: ct);
                await UniTask.WaitUntil(() => IsIn() is true, cancellationToken: ct);
                _audioSources.GetNew().Raise(_audioClips.Voice.DaughterMistakenDirection, SoundType.Voice);
                // 音源の最後が無音になっているので、待機時間0でも良さそう
                await UniTask.WaitForSeconds(_audioClips.Voice.DaughterMistakenDirection.length, cancellationToken: ct);
            }
        }
    }
}