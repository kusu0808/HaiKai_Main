using General;
using Sirenix.OdinInspector;
using SO;
using UnityEngine;

namespace Title
{
    public sealed class TitleBGM : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly]
        private AudioSource _audioSource;

        private void Awake() => _audioSource.Raise(SAudioClips.Entity.BGM.Title, SoundType.BGM);
    }
}