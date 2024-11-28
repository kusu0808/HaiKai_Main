using UnityEngine;
using SO;
using Sirenix.OdinInspector;
using Main.Eventer;
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
        [SerializeField] private RotateDoor _toiletDoor;
        [SerializeField] private SlideDoor _warehouseLookedDoor;
        [SerializeField] private UIElements _uiElements;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private readonly PlayerItem _playerItem = new();

        private AudioSource _yatsuKnockToiletDoorAudioSource = null;
    }
}