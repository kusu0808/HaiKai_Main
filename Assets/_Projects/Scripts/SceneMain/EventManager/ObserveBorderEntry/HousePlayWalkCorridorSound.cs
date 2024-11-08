using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Main.Eventer;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid HousePlayWalkCorridorSound(CancellationToken ct)
        {
            Borders.MultiBorders cache = _borders.HouseCorridor;

            bool IsMovingOnCorridor() => cache.Elements.IsInAny(_player.Position) is true && _player.IsMoving is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnCorridor() is true, cancellationToken: ct);
                AudioSource audioSource = _audioSources.GetNew();
                audioSource.Raise(_audioClips.BGM.WalkOnCorridorInHouse, SoundType.BGM);
                await UniTask.WaitUntil(() => IsMovingOnCorridor() is false, cancellationToken: ct);
                audioSource.Stop();
            }
        }
    }
}