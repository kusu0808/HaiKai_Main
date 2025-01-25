using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace General
{
    public sealed class PostProcessTrigger : MonoBehaviour
    {
        [SerializeField]
        private PostProcessTriggerReference _reference;

#if UNITY_EDITOR
        [Button("開発用のポストプロセスに切り替える", ButtonSizes.Gigantic)]
        private void SetToDevelop() => _reference?.SetState(State.Develop);

        [Button("リリース用のポストプロセスに切り替える", ButtonSizes.Gigantic)]
        private void SetToRelease() => _reference?.SetState(State.Release);

        public enum State
        {
            Develop,
            Release
        }
#endif
        private void Awake() => _reference?.StartTransition(destroyCancellationToken);
    }

    [Serializable]
    public sealed class PostProcessTriggerReference
    {
        [SerializeField, Range(1, 9999), Tooltip("日が完全に暮れるまでの秒数")]
        private float _duration;

        [SerializeField]
        private Light _sun;

        [SerializeField]
        private Volume _postProcessVolume;

        [SerializeField]
        private Light _headLight;

#if UNITY_EDITOR
        public void SetState(PostProcessTrigger.State state)
        {
            if (_sun == null) return;
            if (_postProcessVolume == null) return;
            if (_headLight == null) return;

            switch (state)
            {
                case PostProcessTrigger.State.Develop:
                    {
                        RenderSettings.ambientIntensity = 1.0f;
                        _headLight.enabled = false;
                        _sun.enabled = true;
                        _postProcessVolume.enabled = false;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca)) ca.active = false;
                    }
                    break;

                case PostProcessTrigger.State.Release:
                    {
                        RenderSettings.ambientIntensity = 0.0f;
                        _headLight.enabled = true;
                        _sun.enabled = false;
                        _postProcessVolume.enabled = true;
                        if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca)) ca.active = true;
                    }
                    break;
            }
        }
#endif

        public void StartTransition(CancellationToken ct)
        {
            if (_sun == null) return;
            if (_postProcessVolume == null) return;
            if (_headLight == null) return;

            RenderSettings.ambientIntensity = 1.0f;
            _sun.transform.eulerAngles = new Vector3(50, 330, 0);
            _headLight.enabled = false;
            _postProcessVolume.enabled = false;
            if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca)) ca.active = false;

            DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, 0, _duration)
                .ToUniTask(cancellationToken: ct).Forget();
            DOTween.To(() => _sun.transform.eulerAngles.x, x => _sun.transform.eulerAngles = new Vector3(x, 330, 0), -50, _duration)
                .ToUniTask(cancellationToken: ct).Forget();
            Task(ct).Forget();

            async UniTaskVoid Task(CancellationToken ct)
            {
                await UniTask.WaitForSeconds(_duration * 0.5f, cancellationToken: ct);
                _headLight.enabled = true;
                _postProcessVolume.enabled = true;
                if (_postProcessVolume.profile.TryGet(out ColorAdjustments ca)) ca.active = true;
                await UniTask.WaitForSeconds(_duration * 0.5f, cancellationToken: ct);
            }
        }
    }
}
