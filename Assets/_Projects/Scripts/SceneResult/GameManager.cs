using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Result
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _goToTitleText;

        private void OnEnable() => GoToTitle(destroyCancellationToken).Forget();

        private async UniTaskVoid GoToTitle(CancellationToken ct)
        {
            for (int i = 10; i >= 0; i--)
            {
                if (i <= 0) Scene.ID.Title.LoadAsync().Forget();
                if (_goToTitleText != null) _goToTitleText.text =
                    $"脱出成功！\n<size=72> </size>\n<size=108>({i}秒後にタイトルに戻ります...)</size>";
                await UniTask.WaitForSeconds(1, cancellationToken: ct);
            }
        }
    }
}