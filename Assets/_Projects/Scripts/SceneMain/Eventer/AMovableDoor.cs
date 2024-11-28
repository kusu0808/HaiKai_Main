using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public abstract class AMovableDoor
    {
        protected sealed class MonoBehaviourComponent : MonoBehaviour { }

        [SerializeField, Required, SceneObjectsOnly, Tooltip("ドアのTransform")]
        protected Transform _doorTf;

        [SerializeField, Required, Tooltip("補間方法")]
        protected Ease _ease;

        [SerializeField, Range(0.1f, 20.0f), Tooltip("アニメーション時間")]
        protected float _duration;

        public async virtual UniTask PlayDoorOnce(CancellationToken ct)
        {
            if (_doorTf == null) return;

            await UniTask.Yield(cancellationToken: ct);
        }
    }
}
