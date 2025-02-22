using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using IA;
using UnityEngine;

namespace Init
{
    public sealed class GameManager : MonoBehaviour
    {
        private void OnEnable() => GoToTitle(destroyCancellationToken).Forget();

        private async UniTaskVoid GoToTitle(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => InputGetter.Instance.PlayerAction.Bool, cancellationToken: ct);
            Scene.ID.Title.LoadAsync().Forget();
        }
    }
}