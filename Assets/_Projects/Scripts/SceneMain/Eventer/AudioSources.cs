using System;
using General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class AudioSources
    {
        [SerializeField, Required, SceneObjectsOnly, LabelText("ルート"), Tooltip("この配下の子オブジェクトは全て、AudioSourceコンポーネントのみを持っている前提")]
        private GameObject _root;

        private AudioSource[] _audioSources = null;

        public AudioSource GetNew()
        {
            if (_root == null) return null;

            if (_audioSources is null)
            {
                Transform rootTf = _root.transform;
                int childNum = rootTf.childCount;
                if (childNum < 10) { "AudioSourceの数が少なすぎます".Error(); return null; }
                _audioSources = new AudioSource[childNum];

                for (int i = 0; i < childNum; i++)
                {
                    try
                    {
                        _audioSources[i] = rootTf.GetChild(i).GetComponent<AudioSource>();
                    }
                    catch (Exception) { continue; }
                }
            }

            if (_audioSources is null) return null;

            foreach (AudioSource audioSource in _audioSources)
            {
                if (audioSource.isPlaying is false) return audioSource;
            }

            AudioSource newAudioSource = _root.AddComponent<AudioSource>();
            int len = _audioSources.Length;
            Array.Resize(ref _audioSources, len + 1);
            _audioSources[len] = newAudioSource;
            return newAudioSource;
        }
    }
}