using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using UnityEngine;
using MultiBorders = Main.Eventer.Borders.MultiBorders;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid HousePlayWalkTatamiSound(CancellationToken ct)
        {
            MultiBorders cache = _borders.HouseTatami;

            bool IsMovingOnCorridor() => cache.IsInAny(_player.Position) is true && _player.IsMoving is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnCorridor() is true, cancellationToken: ct);
                AudioSource audioSource = _audioSources.GetNew();
                audioSource.Raise(_audioClips.BGM.WalkOnTatamiInHouse, SoundType.BGM);
                await UniTask.WaitUntil(() => IsMovingOnCorridor() is false, cancellationToken: ct);
                audioSource.Stop();
            }
        }
    }
}