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
                        RenderSettings.ambientIntensity = 1.0f;
                        _headLight.enabled = false;
                        _sun.enabled = true;
                        _postProcessVolume.enabled = false;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca))
                        {
                            ca.active = false;
                        }
                    }
                    break;

                case State.Release:
                    {
                        RenderSettings.ambientIntensity = 0.0f;
                        _headLight.enabled = true;
                        _sun.enabled = false;
                        _postProcessVolume.enabled = true;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca))
                        {
                            ca.active = true;
                            ca.contrast.value = 7.0f;
                            ca.colorFilter.value = new Color32(255, 255, 255, 255);
                        }
                    }
                    break;
            }
        }

        private static readonly float TransitionDuration = 90.0f;

        public void StartTransition(CancellationToken ct)
        {
#if UNITY_EDITOR
            if (_useReleaseOnEditor is false) return;
#else
            if (_useReleaseOnBuild is false) return;
#endif

            if (_postProcessVolume == null) return;
            if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca) is false) return;

            DOTween.To(() => ca.contrast.value, x => ca.contrast.value = x, 64.0f, TransitionDuration).ToUniTask(cancellationToken: ct).Forget();
            DOTween.To(() => ca.colorFilter.value, x => ca.colorFilter.value = x, new Color32(42, 45, 55, 255), TransitionDuration).ToUniTask(cancellationToken: ct).Forget();
        }
    }
}