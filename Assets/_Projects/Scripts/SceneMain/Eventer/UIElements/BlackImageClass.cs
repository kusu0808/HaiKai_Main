using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.UIElements
{
    [Serializable]
    public sealed class BlackImageClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Image _blackImage;

        private bool _isDoingFade = false;

        private void SetAlpha(float alpha)
        {
            if (_blackImage == null) return;
            Color color = _blackImage.color;
            color.a = alpha;
            _blackImage.color = color;
        }

        public async UniTask FadeOut(float duration, CancellationToken ct, Ease ease = Ease.Linear)
        {
            if (_isDoingFade is true) return;
            if (_blackImage == null) return;

            _isDoingFade = true;
            SetAlpha(0);
            await _blackImage.DOFade(1, duration).SetEase(ease).ToUniTask(cancellationToken: ct);
            _isDoingFade = false;
        }

        public async UniTask FadeIn(float duration, CancellationToken ct, Ease ease = Ease.Linear)
        {
            if (_isDoingFade is true) return;
            if (_blackImage == null) return;

            _isDoingFade = true;
            SetAlpha(1);
            await _blackImage.DOFade(0, duration).SetEase(ease).ToUniTask(cancellationToken: ct);
            _isDoingFade = false;
        }
    }
}