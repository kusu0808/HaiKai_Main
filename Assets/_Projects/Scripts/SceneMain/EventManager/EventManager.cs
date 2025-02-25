﻿using Cysharp.Threading.Tasks;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OnEnable() => Observe(destroyCancellationToken).Forget();
        private void OnDisable()
        {
            _dispose?.Invoke();
            _dispose = null;
        }

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            await Initialize(ct);
            ObserveActionAgainstCollider(ct).Forget();
            ObserveBorderEntry(ct);
            ObserveLookingObject(ct).Forget();
        }
    }
}
