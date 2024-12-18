using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ES3Types;
using General;
using IA;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ObserveRaycast(CancellationToken ct)
        {
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

        }
    }
}