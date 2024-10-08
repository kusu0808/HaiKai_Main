using UnityEngine;
using Cysharp.Threading.Tasks;
using General;
using System.Threading;
using System.Collections.Generic;

namespace Hot
{
    public static class EventID
    {
        public static readonly int N1 = 0;
        public static readonly int N2 = 1;
        public static readonly int N3 = 2;

        public static IEnumerable<int> Enumerate()
        {
            yield return N1;
            yield return N2;
            yield return N3;
        }
    }

    public class Border
    {
        public bool IsIn(Vector3 vec) => true;
    }

    public class HitCheck : MonoBehaviour
    {
        [SerializeField] private Border[] borders;

        private void Start() => HandleEvent(destroyCancellationToken).Forget();

        private async UniTaskVoid HandleEvent(CancellationToken ct)
        {
            foreach (int eventID in EventID.Enumerate())
            {
                await UniTask.WaitUntil(() => CheckCollision(eventID), cancellationToken: ct);
                await Do(eventID, ct);
            }
        }

        private bool CheckCollision(int eventID) => borders[eventID].IsIn(transform.position);

        private async UniTask Do(int eventID, CancellationToken ct)
        {
            eventID.Show();
        }
    }
}