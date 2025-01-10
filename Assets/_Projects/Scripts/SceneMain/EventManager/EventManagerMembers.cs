using UnityEngine;
using SO;
using Sirenix.OdinInspector;
using Main.Eventer;
using Main.Eventer.Objects;
using Main.Eventer.Borders;
using Main.Eventer.PlayerChasingCharacter;
using Main.Eventer.UIElements;

namespace Main.EventManager
{
    public sealed partial class EventManager : MonoBehaviour
    {
        [SerializeField] private Debug _debug;
        [Space(25)]
        [SerializeField, AssetsOnly, InlineEditor(InlineEditorModes.FullEditor)] private SAudioClips _audioClips;
        [SerializeField, SceneObjectsOnly] private AudioSources _audioSources;
        [Space(25)]
        [SerializeField] private PlayerCollision _playerCollision;
        [SerializeField] private Objects _objects;
        [SerializeField] private Points _points;
        [SerializeField] private Borders _borders;
        [SerializeField] private Player _player;
        [SerializeField] private Daughter _daughter;
        [SerializeField] private Yatsu _yatsu;
        [SerializeField] private UIElements _uiElements;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private AudioSource _yatsuKnockToiletDoorAudioSource = null;

        private bool _hasRunAwayFromFirstYatsu = false; // 最初にヤツから逃げ切った段階でtrueになる
        private bool _isOpenToiletLockedDoorEventEnabled = true;
        private bool _isPickUpSecretKeyEventEnabled = true;
        private bool _hasSavedDaughter = false; // 娘を助けた段階でtrueになる
    }
}