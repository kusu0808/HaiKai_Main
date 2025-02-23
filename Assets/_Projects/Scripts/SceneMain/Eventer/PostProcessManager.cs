using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Main.Eventer
{
    [Serializable]
    public sealed class PostProcessManager
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Light _sun;

        [SerializeField, Required, SceneObjectsOnly]
        private Volume _postProcessVolume;

        [SerializeField, Required, SceneObjectsOnly]
        private Light _headLight;

        [SerializeField, Tooltip("トランジションのイージング(おすすめはOutQuad)")]
        private Ease _ease;

        [Space(25)]

        [Tooltip("ビルド、実行時 : 開発用を使用"), ReadOnly, ShowInInspector] private static readonly bool _useReleaseOnBuild = true;
        [SerializeField, Tooltip("エディタ、実行時 : trueならリリース用を、falseなら開発用を使用")] private bool _useReleaseOnEditor;
        [Button("エディタ、未実行時 : リリース用に切り替え", ButtonSizes.Small)] private void SetToRelease() => SetState(State.Release);
        [Button("エディタ、未実行時 : 開発用に切り替え", ButtonSizes.Small)] private void SetToDevelop() => SetState(State.Develop);

        private enum State
        {
            Develop,
            Release
        }

        private static readonly (float Shallow, float Deep) AoIntensity = (1.0f, 0.8f);
        private static readonly (float Shallow, float Deep) CaContrast = (7.0f, 10.0f);
        private static readonly (Color32 Shallow, Color32 Deep, Color32 LastEscape) CaColorFilter
            = (new Color32(255, 255, 255, 255), new Color32(84, 90, 110, 255), new Color32(126, 135, 165, 255));
        private static readonly (float Shallow, float Deep) FogDensity = (0.0f, 0.5f);
        private static readonly float GameStartTransitionDuration = 90.0f;
        private static readonly float LastEscapeTransitionDuration = 8.0f;
        private static readonly float FogTransitionDuration = 5.0f;

        /// <summary>
        /// 最初に呼んでほしい
        /// </summary>
        public void Init()
        {
#if UNITY_EDITOR
            DoEditor();
#else
            DoBuild();
#endif
        }

        private void DoEditor()
        {
            if (_useReleaseOnEditor) SetState(State.Release);
            else SetState(State.Develop);
        }

        private void DoBuild()
        {
            if (_useReleaseOnBuild) SetState(State.Release);
            else SetState(State.Develop);
        }

        private void SetState(State state)
        {
            if (_sun == null) return;
            if (_postProcessVolume == null) return;
            if (_headLight == null) return;

            switch (state)
            {
                case State.Develop:
                    {
                        RenderSettings.ambientIntensity = AoIntensity.Shallow;
                        RenderSettings.fogDensity = FogDensity.Shallow;
                        _headLight.enabled = false;
                        _sun.enabled = true;
                        _postProcessVolume.enabled = false;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca))
                        {
                            ca.active = false;
                            ca.contrast.value = CaContrast.Shallow;
                            ca.colorFilter.value = CaColorFilter.Shallow;
                        }
                    }
                    break;

                case State.Release:
                    {
                        RenderSettings.ambientIntensity = AoIntensity.Deep;
                        RenderSettings.fogDensity = FogDensity.Shallow;
                        _headLight.enabled = true;
                        _sun.enabled = false;
                        _postProcessVolume.enabled = true;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca))
                        {
                            ca.active = true;
                            ca.contrast.value = CaContrast.Shallow;
                            ca.colorFilter.value = CaColorFilter.Shallow;
                        }
                    }
                    break;
            }
        }

        public async UniTaskVoid DoGameStartTransition(CancellationToken ct)
        {
#if UNITY_EDITOR
            if (_useReleaseOnEditor is false) return;
#else
            if (_useReleaseOnBuild is false) return;
#endif

            if (_postProcessVolume == null) return;
            if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca) is false) return;

            await UniTask.WhenAll(
                DOTween.To(() => ca.contrast.value, x => ca.contrast.value = x, CaContrast.Deep, GameStartTransitionDuration)
                    .SetEase(_ease)
                    .ToUniTask(cancellationToken: ct),
                DOTween.To(() => ca.colorFilter.value, x => ca.colorFilter.value = x, CaColorFilter.Deep, GameStartTransitionDuration)
                    .SetEase(_ease)
                    .ToUniTask(cancellationToken: ct)
            );
        }

        public async UniTaskVoid DoLastEscapeTransition(CancellationToken ct)
        {
#if UNITY_EDITOR
            if (_useReleaseOnEditor is false) return;
#else
            if (_useReleaseOnBuild is false) return;
#endif

            if (_sun != null) _sun.enabled = true;

            if (_postProcessVolume == null) return;
            if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca) is false) return;

            await UniTask.WhenAll(
                DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, AoIntensity.Shallow, LastEscapeTransitionDuration)
                    .SetEase(_ease)
                    .ToUniTask(cancellationToken: ct),
                DOTween.To(() => ca.contrast.value, x => ca.contrast.value = x, CaContrast.Shallow, LastEscapeTransitionDuration)
                    .SetEase(_ease)
                    .ToUniTask(cancellationToken: ct),
                DOTween.To(() => ca.colorFilter.value, x => ca.colorFilter.value = x, CaColorFilter.LastEscape, LastEscapeTransitionDuration)
                    .SetEase(_ease)
                    .ToUniTask(cancellationToken: ct)
            );

            if (_headLight != null) _headLight.enabled = false;
        }

        public async UniTaskVoid DoFogTransition(bool isGenerate, CancellationToken ct)
        {
#if UNITY_EDITOR
            if (_useReleaseOnEditor is false) return;
#else
            if (_useReleaseOnBuild is false) return;
#endif

            await DOTween.To(
                () => RenderSettings.fogDensity,
                x => RenderSettings.fogDensity = x,
                isGenerate ? FogDensity.Deep : FogDensity.Shallow,
                FogTransitionDuration
            )
            .SetEase(_ease)
            .ToUniTask(cancellationToken: ct);
        }
    }
}