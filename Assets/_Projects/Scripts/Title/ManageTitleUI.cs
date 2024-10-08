using UnityEngine;
using UnityEngine.UI;
using General;

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
            _startButton.onClick.AddListener(StartFromBeginning);
            _continueButton.onClick.AddListener(ContinueAndStart);
            _settingButton.onClick.AddListener(_triggerSettingUI.Open);
            _quitButton.onClick.AddListener(QuitGame);
        }

        /// <summary>
        /// 後方互換
        /// </summary>
        private void StartFromBeginning() => UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        /// <summary>
        /// 後方互換
        /// </summary>
        private void ContinueAndStart() => UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        /// <summary>
        /// 後方互換
        /// </summary>
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