using UnityEngine;
using Main.Eventer;
using SO;
using Sirenix.OdinInspector;
using Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
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
        [SerializeField] private UIElements _uiElements;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private readonly PlayerItem _playerItem = new();
    }
}