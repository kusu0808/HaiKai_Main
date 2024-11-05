using System;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    public enum SoundType
    {
        Master,
        BGM,
        SE
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
                source.playOnAwake = false;
                source.loop = true;

                source.clip = clip;
                source.Play();
            }
            else if (type is SoundType.SE)
            {
                source.playOnAwake = false;
                source.loop = false;

                source.clip = clip; // 一応
                source.PlayOneShot(clip);
            }
            else throw new Exception("無効な種類です");
        }

        public static float BGMVolume
        {
            get => GetVolume(SoundType.BGM);
            set => SetVolume(SoundType.BGM, value);
        }

        public static float SEVolume
        {
            get => GetVolume(SoundType.SE);
            set => SetVolume(SoundType.SE, value);
        }

        public static AudioMixer AM { get; set; } = null;

        private static float GetVolume(SoundType type)
        {
            if (AM == null) return 0;
            AM.GetFloat(type.ToParamNameString(), out float volume);
            return volume;
        }

        private static void SetVolume(SoundType type, float newVolume)
        {
            if (AM == null) return;
            AM.SetFloat(type.ToParamNameString(), newVolume);
        }

        private static string ToParamNameString(this SoundType type) => type switch
        {
            SoundType.Master => "MasterParam",
            SoundType.BGM => "BGMParam",
            SoundType.SE => "SEParam",
            _ => throw new Exception("無効な種類です")
        };
    }
}