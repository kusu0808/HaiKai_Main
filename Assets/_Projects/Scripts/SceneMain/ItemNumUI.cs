using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.UIElements
{
    public sealed class ItemNumUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemNum;
        CancellationTokenSource _cts;

        public void ChangeItemNum(int itemNum)
        {
            if (_itemNum == false) return;
            _itemNum.text = $"Item   {itemNum}/3";

            _cts = new();
            FadeOutAsync(_cts.Token).Forget();
        }

        private async UniTaskVoid FadeOutAsync(CancellationToken token)
        {
            _itemNum.color = Color.white;
            float t = 0f;
            while (t < 3f)
            {
                t += Time.deltaTime;
                _itemNum.color = new Color(1f, 1f, 1f, 1f - (t / 3f));
                await UniTask.Yield(token);
            }
            _itemNum.color = Color.clear;
        }

        private void OnDisable()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}

