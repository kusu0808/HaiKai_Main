using Cysharp.Threading.Tasks;
using General;
using IA;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    /// <summary>
    /// 最初にRollItem()を呼ぶこと
    /// </summary>
    public class ManagePlayerUI : MonoBehaviour
    {
        [SerializeField] private Canvas _playerUICanvas;
        [SerializeField] private Image[] _itemImages;

        private LoopedInt _itemIndex;

        private GameObject _playerUI => _playerUICanvas.gameObject;
        private float _selectInput => InputGetter.Instance.PlayerSelect.Float;

        private static readonly Color32 LightWhite = new(255, 255, 255, 100);
        private static readonly Color32 DarkWhite = new(255, 255, 255, 255);

        public async UniTaskVoid RollItem(CancellationToken ct)
        {
            _playerUI.SetActive(true);

            _itemIndex = new(_itemImages.Length, 0);
            UpdateItemImages(Array.AsReadOnly(_itemImages), 0);

            while (true)
            {
                await UniTask.WaitUntil(() => _selectInput != 0.0f && PauseState.IsPaused is false, cancellationToken: ct);
                _itemIndex.Value += _selectInput > 0 ? -1 : 1;
                UpdateItemImages(Array.AsReadOnly(_itemImages), _itemIndex.Value);
            }

            static void UpdateItemImages(ReadOnlyCollection<Image> itemImages, int itemIndex)
            {
                for (var i = 0; i < itemImages.Count; i++)
                {
                    Image img = itemImages[i];
                    if (img == null) continue;
                    img.color = i == itemIndex ? DarkWhite : LightWhite;
                }
            }
        }
    }
}