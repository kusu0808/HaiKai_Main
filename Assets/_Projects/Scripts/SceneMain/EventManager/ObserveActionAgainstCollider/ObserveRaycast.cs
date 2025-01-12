using System.Threading;
using Cysharp.Threading.Tasks;
using Main.Eventer.UIElements;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ObserveRaycast(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.NextFrame(ct);

                Collider collider = _player.GetHitColliderFromCamera();
                if (collider == null)
                {
                    _uiElements.Reticle.Color = ReticleClass.ColorNormal;
                    continue;
                }
                _uiElements.Reticle.Color =
                    collider.tag.Contains("ActionAgainstCollider") ?
                        ReticleClass.ColorActionAgainstCollider : ReticleClass.ColorNormal;
            }
        }
    }
}