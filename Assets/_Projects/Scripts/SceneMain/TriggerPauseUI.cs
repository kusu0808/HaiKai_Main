using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using IA;
using General;
using TMPro;
using StarterAssets;
using Main.EventManager;

namespace Main
{
    public sealed class TriggerPauseUI : MonoBehaviour
    {
        [Header("Pause UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Canvas _pauseCanvas;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _toTitleButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _settingButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _closeButton;

        [Header("Setting UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Canvas _settingCanvas;

        [SerializeField, Required, SceneObjectsOnly]
        private Slider _bgmVolumeSlider;

        [SerializeField, Required, SceneObjectsOnly]
        private Slider _seVolumeSlider;

        [SerializeField, Required, SceneObjectsOnly]
        private Slider _mouseSensitivitySlider;

        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _bgmVolumeText;

        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _seVolumeText;

        [SerializeField, Required, SceneObjectsOnly]
        private TextMeshProUGUI _mouseSensitivityText;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _closeSettingButton;

        [Header("To Title UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Canvas _toTitleCanvas;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _yesButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _noButton;

        [Header("Others")]

        [SerializeField, Required, SceneObjectsOnly]
        private FirstPersonController _firstPersonController;

        private static float _mouseSensitivity = EventManagerConst.RotationSpeedInit;
        private static float mouseSensitivity
        {
            get => _mouseSensitivity;
            set // 内部で変更→視点感度が変わる
            {
                _mouseSensitivity = value;
                OnMouseSensitivityChangedFromInside?.OnNext(value);
            }
        }
        public static float MouseSensitivity
        {
            get => _mouseSensitivity;
            set // 外部で変更→視点感度は変わらない（外部で変更すること）
            {
                _mouseSensitivity = value;
                OnMouseSensitivityChangedFromOutside?.OnNext(value);
            }
        }
        private static Subject<float> OnMouseSensitivityChangedFromInside { get; set; } = new Subject<float>();
        private static Subject<float> OnMouseSensitivityChangedFromOutside { get; set; } = new Subject<float>();

        public static Subject<Unit> OnPauseBegin { get; set; } = new Subject<Unit>();
        public static Subject<Unit> OnPauseEnd { get; set; } = new Subject<Unit>();

        private enum State
        {
            OnGame,
            PauseUI,
            SettingUI,
            ToTitleUI
        }

        private State _state;

        // UIを切り替え、ポーズとカーソルの状態を更新、ポーズ開始/終了時ならイベントを発火
        private void ChangeUI(State state)
        {
            if (_pauseCanvas != null) _pauseCanvas.gameObject.SetActive(state == State.PauseUI);
            if (_settingCanvas != null) _settingCanvas.gameObject.SetActive(state == State.SettingUI);
            if (_toTitleCanvas != null) _toTitleCanvas.gameObject.SetActive(state == State.ToTitleUI);

            if (state == State.OnGame)
            {
                PauseState.IsPaused = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PauseState.IsPaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (_state == State.OnGame && state != State.OnGame) OnPauseBegin?.OnNext(Unit.Default);
            else if (_state != State.OnGame && state == State.OnGame) OnPauseEnd?.OnNext(Unit.Default);

            _state = state;
        }

        private void OnEnable()
        {
            _state = State.OnGame;
            ChangeUI(State.OnGame);

            _bgmVolumeSlider.value = SoundManager.BGMVolume;
            _seVolumeSlider.value = SoundManager.SEVolume;
            _mouseSensitivitySlider.value = mouseSensitivity;
            _bgmVolumeText.text = SoundManager.BGMVolume.ToString("F0");
            _seVolumeText.text = SoundManager.SEVolume.ToString("F0");
            _mouseSensitivityText.text = mouseSensitivity.ToString("F1");
            if (_firstPersonController != null) _firstPersonController.RotationSpeed = mouseSensitivity;
            OnMouseSensitivityChangedFromInside.Subscribe(value =>
            {
                _mouseSensitivityText.text = value.ToString("F1");
            }).AddTo(this);
            OnMouseSensitivityChangedFromOutside.Subscribe(value =>
            {
                _mouseSensitivitySlider.value = value;
                _mouseSensitivityText.text = value.ToString("F1");
            }).AddTo(this);

            _toTitleButton.onClick.AddListener(() => ChangeUI(State.ToTitleUI));
            _settingButton.onClick.AddListener(() => ChangeUI(State.SettingUI));
            _closeButton.onClick.AddListener(() => ChangeUI(State.OnGame));

            _bgmVolumeSlider.onValueChanged.AddListener(value =>
            {
                SoundManager.BGMVolume = value;
                _bgmVolumeText.text = value.ToString("F0");
            });
            _seVolumeSlider.onValueChanged.AddListener(value =>
            {
                SoundManager.VoiceVolume = value;
                SoundManager.SEVolume = value;
                SoundManager.SERoughVolume = value;
                _seVolumeText.text = value.ToString("F0");
            });
            _mouseSensitivitySlider.onValueChanged.AddListener(value =>
            {
                mouseSensitivity = value;
                if (_firstPersonController != null) _firstPersonController.RotationSpeed = value;
            });
            _closeSettingButton.onClick.AddListener(() => ChangeUI(State.PauseUI));

            _yesButton.onClick.AddListener(() => Scene.ID.Title.LoadAsync().Forget());
            _noButton.onClick.AddListener(() => ChangeUI(State.PauseUI));
        }

        private void Update()
        {
            if (InputGetter.Instance.Pause.Bool)
            {
                switch (_state)
                {
                    case State.OnGame:
                        ChangeUI(State.PauseUI);
                        break;
                    case State.PauseUI:
                        ChangeUI(State.OnGame);
                        break;
                    case State.SettingUI:
                        ChangeUI(State.OnGame);
                        break;
                    case State.ToTitleUI:
                        ChangeUI(State.OnGame);
                        break;
                }
            }
            else if (InputGetter.Instance.PlayerCancel.Bool)
            {
                switch (_state)
                {
                    case State.OnGame:
                        break;
                    case State.PauseUI:
                        ChangeUI(State.OnGame);
                        break;
                    case State.SettingUI:
                        ChangeUI(State.PauseUI);
                        break;
                    case State.ToTitleUI:
                        ChangeUI(State.PauseUI);
                        break;
                }
            }
        }
    }
}