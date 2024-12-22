using Cysharp.Threading.Tasks;
using System.Threading;
using BorderSystem;
using General;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid VillageFarWayYatsuStepOnGlassPiece(CancellationToken ct)
        {
            if (_objects.VillageFarWayScatteredGlassPiece.IsEnabled is false) return;

            Border cache = _borders.VillageFarWayYatsuStepOnGlassPiece;

            bool IsMovingOnClassPiece() => cache.IsIn(_yatsu.Position) is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnClassPiece() is true, cancellationToken: ct);
                AudioSource audioSource = _audioSources.GetNew();
                audioSource.Raise(_audioClips.BGM.BridgeCreak, SoundType.BGM);
                await UniTask.WaitUntil(() => IsMovingOnClassPiece() is false, cancellationToken: ct);
                audioSource.Stop();
            }
        }
    }
}