using System;
using SO;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    public enum SoundType
    {
        Master,
        BGM,
        Voice,
        SE,
        SERough
    }

    public static class SoundManager
    {
        /// <summary>
        /// nullチェック済み
        /// SoundType.Masterは不可
        /// </summary>
        public static void Raise(this AudioSource source, AudioClip clip, SoundType type)
        {
            if (source == null) return;
            if (clip == null) return;
            if (type is SoundType.Master) return;

            if (type is SoundType.BGM)
            {
                source.outputAudioMixerGroup = SAudioClips.Entity.BGMMixerGroup;

                source.playOnAwake = false;
                source.loop = true;

                source.clip = clip;
                source.Play();
            }
            else if (type is SoundType.Voice or SoundType.SE or SoundType.SERough)
            {
                source.outputAudioMixerGroup = SAudioClips.Entity.SEMixerGroup;

                source.playOnAwake = false;
                source.loop = false;

                source.clip = clip; // 一応
                source.PlayOneShot(clip);
            }
            else throw new Exception("無効な種類です");
        }

        public static readonly float VolumeMax = 20.0f;
        public static readonly float VolumeMin = -80.0f;

        public static float BGMVolume
        {
            get => GetVolume(SoundType.BGM);
            set => SetVolume(SoundType.BGM, value);
        }

        // Voice, SE, SERoughは同じ
        public static float VoiceVolume
        {
            get => GetVolume(SoundType.Voice);
            set => SetVolume(SoundType.Voice, value);
        }

        // Voice, SE, SERoughは同じ
        public static float SEVolume
        {
            get => GetVolume(SoundType.SE);
            set => SetVolume(SoundType.SE, value);
        }

        // Voice, SE, SERoughは同じ
        public static float SERoughVolume
        {
            get => GetVolume(SoundType.SERough);
            set => SetVolume(SoundType.SERough, value);
        }

        private static AudioMixer AudioMixer => SAudioClips.Entity.AudioMixer;

        private static float GetVolume(SoundType type)
        {
            if (AudioMixer == null) return default;
            string paramName = type.ToParamNameString();
            if (string.IsNullOrEmpty(paramName)) return default;
            AudioMixer.GetFloat(paramName, out float volume);
            return volume;
        }

        private static void SetVolume(SoundType type, float newVolume)
        {
            if (AudioMixer == null) return;
            string paramName = type.ToParamNameString();
            if (string.IsNullOrEmpty(paramName)) return;
            AudioMixer.SetFloat(paramName, newVolume);
        }

        private static string ToParamNameString(this SoundType type) => type switch
        {
            SoundType.Master => "MasterParam",
            SoundType.BGM => "BGMParam",
            SoundType.Voice or SoundType.SE or SoundType.SERough => "SEParam",
            _ => string.Empty
        };
    }
}