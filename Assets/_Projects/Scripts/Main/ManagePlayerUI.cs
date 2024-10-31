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
    /// アイテムの個数や種類などは、他の場所で管理すること
    /// </summary>
    public class ManagePlayerUI : MonoBehaviour
    {
        [SerializeField] private Canvas _playerUICanvas;
        [SerializeField] private Image[] _itemSlots;
        [SerializeField] private Image[] _itemImages;

        private LoopedInt _itemIndex;
        public int ItemIndex => _itemIndex.Value;

        private GameObject _playerUI => _playerUICanvas.gameObject;
        private float _selectInput => InputGetter.Instance.PlayerSelect.Float;

        private static readonly Color32 LightWhite = new(255, 255, 255, 100);
        private static readonly Color32 DarkWhite = new(255, 255, 255, 255);

        public async UniTaskVoid RollItem(CancellationToken ct)
        {
            _playerUI.SetActive(true);

            _itemIndex = new(_itemSlots.Length, 0);
            UpdateItemSlots(Array.AsReadOnly(_itemSlots), 0);

            while (true)
            {
                await UniTask.WaitUntil(() => _selectInput != 0.0f && PauseState.IsPaused is false, cancellationToken: ct);
                _itemIndex.Value += _selectInput > 0 ? -1 : 1;
                UpdateItemSlots(Array.AsReadOnly(_itemSlots), _itemIndex.Value);
            }

            static void UpdateItemSlots(ReadOnlyCollection<Image> itemSlots, int itemIndex)
            {
                for (var i = 0; i < itemSlots.Count; i++)
                {
                    Image img = itemSlots[i];
                    if (img == null) continue;
                    img.color = i == itemIndex ? DarkWhite : LightWhite;
                }
            }
        }

        /// <summary>
        /// 非表示にしたいときは、spriteにnullを入れる
        /// </summary>
        public void SetSprite(int index, Sprite sprite)
        {
            if (_itemImages is null) return;
            if (!(0 <= index && index < _itemImages.Length)) return;

            Image image = _itemImages[index];
            if (image == null) return;

            // index番目にspriteの画像をセット
            image.sprite = sprite;
            SetAlpha(image, (sprite == null) ? 0 : 1);

            static void SetAlpha(Image image, float alpha)
            {
                if (image == null) return;
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}