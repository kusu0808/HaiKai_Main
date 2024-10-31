using UnityEngine;
using Main.Eventer;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        [SerializeField] private Debug _debug;
        [Space(25)]
        [SerializeField] private Points _points;
        [SerializeField] private Borders _borders;
        [SerializeField] private Player _player;
        [SerializeField] private UIElements _uiElements;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private readonly PlayerItem _playerItem = new();
    }
}