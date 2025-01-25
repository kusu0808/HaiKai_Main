using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using IA;
using General;

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

#if false
        [SerializeField, Required, SceneObjectsOnly]
        private Slider _mouseSensitivitySlider;
#endif

        [SerializeField, Required, SceneObjectsOnly]
        private Button _closeSettingButton;

        [Header("To Title UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Canvas _toTitleCanvas;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _yesButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _noButton;

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

            _toTitleButton.onClick.AddListener(() => ChangeUI(State.ToTitleUI));
            _settingButton.onClick.AddListener(() => ChangeUI(State.SettingUI));
            _closeButton.onClick.AddListener(() => ChangeUI(State.OnGame));

            _bgmVolumeSlider.onValueChanged.AddListener(value => SoundManager.BGMVolume = value);
            _seVolumeSlider.onValueChanged.AddListener(value =>
            {
                SoundManager.VoiceVolume = value;
                SoundManager.SEVolume = value;
                SoundManager.SERoughVolume = value;
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