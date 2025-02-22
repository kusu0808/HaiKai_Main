using UnityEngine;
using SO;
using Sirenix.OdinInspector;
using Main.Eventer;
using Main.Eventer.Objects;
using Main.Eventer.Borders;
using Main.Eventer.PlayerChasingCharacter;
using Main.Eventer.UIElements;
using UniRx;

namespace Main.EventManager
{
    public sealed partial class EventManager : MonoBehaviour
    {
        [SerializeField] private Debug _debug;
        [Space(25)]
        [SerializeField, SceneObjectsOnly] private AudioSources _audioSources;
        [SerializeField] private Objects _objects;
        [SerializeField] private Points _points;
        [SerializeField] private Borders _borders;
        [SerializeField] private Player _player;
        [SerializeField] private Daughter _daughter;
        [SerializeField] private Yatsu _yatsu;
        [SerializeField] private UIElements _uiElements;
        [SerializeField] private PostProcessManager _postProcessManager;
        [Space(25)]
        [SerializeField] private BusMover _busMover;

        private SAudioClips _audioClips => SAudioClips.Entity;

        private readonly ReactiveProperty<bool> _isWalkingSoundMuted = new ReactiveProperty<bool>(false);

        private bool _hasReadPuzzleHintScroll = false; // パズルのヒントを読んだ段階でtrueになる
        private bool _hasRunAwayFromFirstYatsu = false; // 最初にヤツから逃げ切った段階でtrueになる
        private bool _hasScoupedDeerBlood = false; // 鹿の血をコップにくんだ段階でtrueになる
        private bool _hasCrashedCup = false; // お地蔵様でコップを割った段階でtrueになる
        private bool _isOpenToiletLockedDoorEventEnabled = true;
        private bool _isPickUpSecretKeyEventEnabled = true;
        private bool _hasDecidedNotToTurnBack = false; // 秘密の鍵を入手後、トイレのドアをくぐった段階でtrueになる
        private bool _hasSavedDaughter = false; // 娘を助けた段階でtrueになる
    }
}