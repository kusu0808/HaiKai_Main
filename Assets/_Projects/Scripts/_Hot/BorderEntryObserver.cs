using BorderSystem;
using Cysharp.Threading.Tasks;
using General;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Hot
{
    public static class EventID
    {
        public static readonly int N1 = 0;
        public static readonly int N2 = 1;
        public static readonly int N3 = 2;

        public static IEnumerable<int> Enumerate()
        {
            for (int i = 0; i <= 2; i++)
            {
                yield return i;
            }
        }
    }

    public sealed class BorderEntryObserver : MonoBehaviour
    {
        [SerializeField] private Border[] borders;

        private void OnEnable() => Observe(destroyCancellationToken).Forget();

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            foreach (int eventID in EventID.Enumerate())
            {
                await UniTask.WaitUntil(() => CheckInclusion(eventID), cancellationToken: ct);
                await Raise(eventID, ct);
            }
        }

        private bool CheckInclusion(int eventID) => borders[eventID].IsIn(transform.position) == true;

        private async UniTask Raise(int eventID, CancellationToken ct)
        {
            // 後方置換

            eventID.Show();
            await UniTask.Delay(1000, cancellationToken: ct);
        }
    }
}