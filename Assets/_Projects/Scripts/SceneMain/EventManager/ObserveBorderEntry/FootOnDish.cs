using Cysharp.Threading.Tasks;
using System.Threading;
using BorderSystem;
using General;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid FootOnDish(CancellationToken ct)
        {
            Border cache = _borders.FootOnDish;

            bool IsMovingOnDish() => cache.IsIn(_player.Position) is true && _player.IsMoving is true;

            while (true)
            {
                await UniTask.WaitUntil(() => IsMovingOnDish() is true, cancellationToken: ct);
                AudioSource audioSource = _audioSources.GetNew();
                audioSource.Raise(_audioClips.BGM.MoveOnBrokenDish, SoundType.BGM);
                await UniTask.WaitUntil(() => IsMovingOnDish() is false, cancellationToken: ct);
                audioSource.Stop();
            }
        }
    }
}