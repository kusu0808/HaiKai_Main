using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Threading;
using UnityEngine;

namespace Player
{
    public sealed class CollisionWaiter : MonoBehaviour
    {
        private void OnEnable() => WaitForCollisionEnter(destroyCancellationToken).Forget();

        private async UniTaskVoid WaitForCollisionEnter(CancellationToken ct)
        {
            await foreach (Collision collision in this.GetAsyncCollisionEnterTrigger().WithCancellation(ct))
            {
                Do(collision.gameObject.tag switch
                {
                    "N0" => () => Tmp(0),
                    "N1" => () => Tmp(1),
                    "N2" => () => Tmp(2),
                    "N3" => () => Tmp(3),
                    "N4" => () => Tmp(4),
                    "N5" => () => Tmp(5),
                    "N6" => () => Tmp(6),
                    "N7" => () => Tmp(7),
                    "N8" => () => Tmp(8),
                    "N9" => () => Tmp(9),
                    _ => null
                });
            }

            static void Do(Action action) => action?.Invoke();
        }

        private void Tmp(int i) => Debug.Log(i);
    }
}