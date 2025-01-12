using UnityEngine;
using UnityEngine.UI;
using General;
using SceneGeneral;

namespace Title
{
    public sealed class ManageTitleUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TriggerSettingUI _triggerSettingUI;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(() => Scene.ID.Main.LoadAsync().Forget());
            _continueButton.onClick.AddListener(() => Scene.ID.Main.LoadAsync().Forget());
            _settingButton.onClick.AddListener(_triggerSettingUI.Open);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}