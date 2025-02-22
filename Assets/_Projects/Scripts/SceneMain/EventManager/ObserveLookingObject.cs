using System.Threading;
using Cysharp.Threading.Tasks;
using Main.Eventer.UIElements;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ObserveLookingObject(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.NextFrame(ct);

                Collider collider = _player.GetHitColliderFromCamera();

                if (collider != null && collider.tag.Contains("ActionAgainstCollider"))
                {
                    _uiElements.Reticle.Color = ReticleClass.ColorActionAgainstCollider;
                    _uiElements.Reticle.Size = ReticleClass.SizeBig;
                }
                else
                {
                    _uiElements.Reticle.Color = ReticleClass.ColorNormal;
                    _uiElements.Reticle.Size = ReticleClass.SizeDefault;
                }
            }
        }
    }
}