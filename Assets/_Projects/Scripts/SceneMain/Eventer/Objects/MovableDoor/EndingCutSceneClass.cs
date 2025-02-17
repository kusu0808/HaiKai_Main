using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class EndingCutSceneClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Image _blackBorderUp;

        [SerializeField, Required, SceneObjectsOnly]
        private Image _blackBorderDown;

        public async UniTaskVoid TranseBlackBorder(CancellationToken ct)
        {
            if (_blackBorderUp == null || _blackBorderDown == null) return;

            await UniTask.WhenAll(
                DOTween
                    .To(
                        () => _blackBorderUp.rectTransform.sizeDelta.y,
                        x => _blackBorderUp.rectTransform.sizeDelta = new Vector2(_blackBorderUp.rectTransform.sizeDelta.x, x),
                        150,
                        0.5f
                    )
                    .SetEase(Ease.Linear)
                    .ToUniTask(cancellationToken: ct),
                DOTween
                    .To(
                        () => _blackBorderDown.rectTransform.sizeDelta.y,
                        x => _blackBorderDown.rectTransform.sizeDelta = new Vector2(_blackBorderDown.rectTransform.sizeDelta.x, x),
                        150,
                        0.5f
                    )
                    .SetEase(Ease.Linear)
                    .ToUniTask(cancellationToken: ct)
            );
        }

        public async UniTaskVoid TranseTimeScale(CancellationToken ct)
        {
            Time.timeScale = 0.5f;
            await UniTask.WaitForSeconds(2.0f, cancellationToken: ct);
            Time.timeScale = 3.0f;
            await UniTask.WaitForSeconds(0.5f, cancellationToken: ct);
            Time.timeScale = 1.0f;
        }
    }
}