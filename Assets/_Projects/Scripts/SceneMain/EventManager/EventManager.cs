using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager : MonoBehaviour
    {
        private bool _isFirstUpdateDone = false;
        private void Update()
        {
            if (_isFirstUpdateDone is false)
            {
                _isFirstUpdateDone = true;
                OnStart();
            }

            OnUpdate();
        }

        private void OnStart()
        {
            Observe(destroyCancellationToken).Forget();
        }

        private void OnUpdate()
        {
            _daughter.ReTargetThisPlayer(_player.Position);
        }

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            await Initialize(ct);
            ObserveActionAgainstCollider(ct).Forget();
            ObserveBorderEntry(ct);
        }
    }
}
