using Cysharp.Threading.Tasks;
using System.Threading;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void OnEnable() => Observe(destroyCancellationToken).Forget();

        private async UniTaskVoid Observe(CancellationToken ct)
        {
            await Initialize(ct);
            ObserveActionAgainstCollider(ct).Forget();
            ObserveBorderEntry(ct);
        }
    }
}
