using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid __ObserveRaycast(CancellationToken ct)
        {
            // よく分からないのでオフにしておく
#if false
            while (true)
            {
                await UniTask.Yield(ct);

                Collider collider = _player.GetHitColliderFromCamera();
                if (collider == null) {
                    _uiElements.Reticle.ChangeColor(false);
                    continue;
                }

                bool flag = IfChangeReticle(collider.tag);
                if (flag == false){
                    _uiElements.Reticle.ChangeColor(false);
                    continue;
                }

                _uiElements.Reticle.ChangeColor(true);
            }

            static bool IfChangeReticle(string tag) => tag switch
            {
                "ActionAgainstCollider/Message/BusSign" => true,
                "ActionAgainstCollider/Event/DaughterKnife" => true,
                _ => false
            };
#else
            await UniTask.NextFrame(ct);
#endif
        }
    }
}