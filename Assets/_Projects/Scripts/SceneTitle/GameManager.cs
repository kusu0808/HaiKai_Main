using General;
using IA;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("Main UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Button _startButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _settingButton;

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

        [Header("Start UI")]

        [SerializeField, Required, SceneObjectsOnly]
        private Canvas _startCanvas;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _yesButton;

        [SerializeField, Required, SceneObjectsOnly]
        private Button _noButton;

        private enum State
        {
            TitleUI,
            SettingUI,
            StartUI
        }

        private State _state;

        // UIを切り替え
        private void ChangeUI(State state)
        {
            if (_startCanvas != null) _startCanvas.gameObject.SetActive(state == State.StartUI);
            if (_settingCanvas != null) _settingCanvas.gameObject.SetActive(state == State.SettingUI);

            _state = state;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _state = State.TitleUI;
            ChangeUI(State.TitleUI);

            _startButton.onClick.AddListener(() => ChangeUI(State.StartUI));
            _settingButton.onClick.AddListener(() => ChangeUI(State.SettingUI));

            _bgmVolumeSlider.onValueChanged.AddListener(value => SoundManager.BGMVolume = value);
            _seVolumeSlider.onValueChanged.AddListener(value =>
            {
                SoundManager.VoiceVolume = value;
                SoundManager.SEVolume = value;
                SoundManager.SERoughVolume = value;
            });
            _closeSettingButton.onClick.AddListener(() => ChangeUI(State.TitleUI));

            _yesButton.onClick.AddListener(() => Scene.ID.Main.LoadAsync().Forget());
            _noButton.onClick.AddListener(() => ChangeUI(State.TitleUI));
        }

        private void Update()
        {
            if (InputGetter.Instance.Pause.Bool)
            {
                switch (_state)
                {
                    case State.TitleUI:
                        break;
                    case State.SettingUI:
                        ChangeUI(State.TitleUI);
                        break;
                    case State.StartUI:
                        ChangeUI(State.TitleUI);
                        break;
                }
            }
            else if (InputGetter.Instance.PlayerCancel.Bool)
            {
                switch (_state)
                {
                    case State.TitleUI:
                        break;
                    case State.SettingUI:
                        ChangeUI(State.TitleUI);
                        break;
                    case State.StartUI:
                        ChangeUI(State.TitleUI);
                        break;
                }
            }
        }
    }
}