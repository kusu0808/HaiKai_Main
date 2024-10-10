using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    /// <summary>
    /// 最初にChangeVolume()を呼ぶこと
    /// </summary>
    public sealed class TriggerSettingUI : MonoBehaviour
    {
        [SerializeField] private Canvas _settingUICanvas;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _seSlider;
        [SerializeField] private Button _closeButton;

        private GameObject _settingUI => _settingUICanvas.gameObject;
        public bool IsActive => _settingUI.activeSelf;

        private static readonly float MaxVolume = 20.0f;
        private static readonly float MinVolume = -20.0f;

        private void OnEnable()
        {
            _settingUI.SetActive(false);
            _closeButton.onClick.AddListener(() => _settingUI.SetActive(false));

            _bgmSlider.value = 0.5f;
            _seSlider.value = 0.5f;
        }

        public async UniTaskVoid ChangeVolume(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WhenAny(
                UniTask.WaitUntilValueChanged(_bgmSlider, sld => sld.value, cancellationToken: ct),
                UniTask.WaitUntilValueChanged(_seSlider, sld => sld.value, cancellationToken: ct));

                SoundManager.BGMVolume = Convert(_bgmSlider);
                SoundManager.SEVolume = Convert(_seSlider);
            }
        }

        private float Convert(Slider slider)
            => slider == null ? 0 : slider.value.Remap(0, 1, MinVolume, MaxVolume);

        public void Open() => _settingUI.SetActive(true);
    }
}