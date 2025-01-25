using General;
using IA;
using Sirenix.OdinInspector;
using TMPro;
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

            _bgmVolumeSlider.value = SoundManager.BGMVolume;
            _seVolumeSlider.value = SoundManager.SEVolume;
            _mouseSensitivitySlider.value = Main.TriggerPauseUI.MouseSensitivity;
            _bgmVolumeText.text = SoundManager.BGMVolume.ToString("F0");
            _seVolumeText.text = SoundManager.SEVolume.ToString("F0");
            _mouseSensitivityText.text = Main.TriggerPauseUI.MouseSensitivity.ToString("F1");

            _startButton.onClick.AddListener(() => ChangeUI(State.StartUI));
            _settingButton.onClick.AddListener(() => ChangeUI(State.SettingUI));

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
                Main.TriggerPauseUI.MouseSensitivity = value;
                _mouseSensitivityText.text = value.ToString("F1");
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