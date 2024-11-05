using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Main
{
    public class ChangeVignette : MonoBehaviour
    {
        [SerializeField] private Volume _volume;

        private static readonly float MinVolume = 0.25f;
        private static readonly float MaxVolume = 0.5f;
        private static readonly float Duration = 0.6f;
        private static readonly float DurationOnCancel = 0.2f;

        /// <summary>
        /// 後方互換
        /// </summary>
        private async void OnEnable()
        {
            CancellationTokenSource cts = new();
            await UniTask.Delay(2000);
            StartAnimation(cts.Token).Forget();
            await UniTask.Delay(5000);
            cts.Cancel();
        }

        public async UniTask StartAnimation(CancellationToken ct)
        {
            if (_volume.profile.TryGet<Vignette>(out var vignette) is false) return;

            try
            {
                vignette.intensity.value = MinVolume;

                while (true)
                {
                    float value = MinVolume;

                    await DOTween.To(() => value, x => value = x, MaxVolume, Duration)
                        .OnUpdate(() => vignette.intensity.value = value)
                        .ToUniTask(cancellationToken: ct);

                    await DOTween.To(() => value, x => value = x, MinVolume, Duration)
                        .OnUpdate(() => vignette.intensity.value = value)
                        .ToUniTask(cancellationToken: ct);
                }
            }
            catch (OperationCanceledException)
            {
                float value = vignette.intensity.value;
                await DOTween.To(() => value, x => value = x, MinVolume, DurationOnCancel)
                        .OnUpdate(() => vignette.intensity.value = value);
            }
        }
    }
}