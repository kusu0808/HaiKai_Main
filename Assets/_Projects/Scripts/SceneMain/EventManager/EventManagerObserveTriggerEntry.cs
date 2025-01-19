using System;
using System.Threading;
using UnityEngine;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OnPlayerTriggerEnter(Collider other, CancellationToken ct)
        {
            if (other == null) return;
            GetAction(other.tag, ct)?.Invoke();

            Action GetAction(string tag, CancellationToken ctIfNeeded) => tag switch
            {
                _ => null
            };
        }
    }
}
