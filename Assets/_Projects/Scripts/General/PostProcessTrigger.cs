using System;
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
#else
        private void Awake() => _reference?.SetState(State.Release);
#endif

        public enum State
        {
            Develop,
            Release
        }
    }

    [Serializable]
    public sealed class PostProcessTriggerReference
    {
        [SerializeField]
        private Light _sun;

        [SerializeField]
        private Volume _postProcessVolume;

        [SerializeField]
        private Light _headLight;

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
    }
}
