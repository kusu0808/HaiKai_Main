using UnityEngine;
using UnityEngine.UI;

namespace Result
{
    public class ManageResultUI : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toTitleButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _toTitleButton.onClick.AddListener(ToTitle);
        }

        /// <summary>
        /// 後方置換
        /// </summary>
        private void RestartGame() => UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        /// <summary>
        /// 後方置換
        /// </summary>
        private void ToTitle() => UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}