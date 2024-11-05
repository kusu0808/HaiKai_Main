using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager : MonoBehaviour
    {
        private void OnEnable() => Observe(destroyCancellationToken).Forget();

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            await Initialize(ct);
            ObserveAction(ct).Forget();
            ObserveTrigger(ct);
        }
    }
}
