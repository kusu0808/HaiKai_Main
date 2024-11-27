using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Main.Eventer
{
    /// <summary>
    /// このインスタンスの参照をもらい、MoveOnceを呼ぶことでバスが移動する
    /// </summary>
    public sealed class BusMover : MonoBehaviour
    {
        [SerializeField, Tooltip("バスのTransform")]
        private Transform _busTf;

        [SerializeField, Tooltip("バスの移動先のTransform\nローカルx座標のみを変化させるべき")]
        private Transform _destTf;

        [SerializeField, Tooltip("補間方法")]
        private Ease _ease;

        [SerializeField, Tooltip("移動時間")]
        private float _duration;

        /// <summary>
        /// 重複した動作は想定していないので、何回も呼んではいけない
        /// </summary>
        public async UniTaskVoid MoveOnce(CancellationToken ct)
        {
            if (_busTf == null) return;

            await _busTf
                .DOLocalMoveX(_destTf.localPosition.x, _duration)
                .SetEase(_ease)
                .ToUniTask(cancellationToken: ct);
        }
    }
}