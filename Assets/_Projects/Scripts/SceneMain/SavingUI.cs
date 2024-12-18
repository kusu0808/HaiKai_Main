using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.UIElements
{
    public sealed class SavingUI : MonoBehaviour
    {
        [SerializeField] private Image _savingImage;
        private CancellationTokenSource _cts;

        void Start()
        {
            _cts = new CancellationTokenSource();
           //SavingAnimation(_cts.Token).Forget();
        }

        private async UniTask SavingAnimation(CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                //0.5sec

                _savingImage.color = Color.white;
                await UniTask.Delay(TimeSpan.FromSeconds(0.25));

                _savingImage.color = Color.clear;
                await UniTask.Delay(TimeSpan.FromSeconds(0.25));
            }
        }

        private void OnDisable()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}

