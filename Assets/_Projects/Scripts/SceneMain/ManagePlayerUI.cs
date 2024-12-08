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
        [SerializeField] private Image[] _itemSlots;
        [SerializeField] private Image[] _itemImages;

        private LoopedInt _itemIndex;

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
        /// ターゲットとするspriteを指定
        /// </summary>
        public void SetSprite(Sprite sprite, bool isShow)
        {
            if (isShow)
            {
                if (sprite == null) return;
                if (_itemImages is null) return;

                foreach (Image img in _itemImages)
                {
                    if (img == null) continue;

                    if (img.sprite != null) continue;
                    img.sprite = sprite;
                    SetAlpha(img, 1);
                    break;
                }
            }
            else
            {
                if (sprite == null) return;
                if (_itemImages is null) return;

                int len = _itemImages.Length;

                for (int i = 0; i < len; i++)
                {
                    Image img = _itemImages[i];
                    if (img == null) continue;

                    if (img.sprite != sprite) continue;
                    img.sprite = null;
                    SetAlpha(img, 0);

                    for (int j = i + 1; j < len; j++)
                    {
                        Image nextImg = _itemImages[j];
                        if (nextImg == null) continue;

                        Sprite nextSprite = nextImg.sprite;
                        if (nextSprite == null) continue;

                        img.sprite = nextImg.sprite;
                        SetAlpha(img, 1);
                        nextImg.sprite = null;
                        SetAlpha(nextImg, 0);
                    }

                    break;
                }
            }

            static void SetAlpha(Image image, float alpha)
            {
                if (image == null) return;
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }

        /// <summary>
        /// spriteがセットされていて、かつそのアイテムをセレクトしているか
        /// </summary>
        public bool IsHolding(Sprite sprite)
        {
            if (_itemImages is null) return false;
            if (sprite == null) return false;

            Sprite nowSprite = _itemImages[_itemIndex.Value].sprite;
            if (nowSprite == null) return false;

            return nowSprite == sprite;
        }
    }
}