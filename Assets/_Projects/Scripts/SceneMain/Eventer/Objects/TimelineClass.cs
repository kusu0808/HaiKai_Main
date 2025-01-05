using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class TimelineClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private GameObject _root;

        [SerializeField, Required, SceneObjectsOnly]
        private PlayableDirector _playableDirector;

        private bool _hasPlayed = false;

        /// <summary>
        /// 一回のみ再生
        /// </summary>
        public async UniTask PlayOnce(CancellationToken ct)
        {
            if (_hasPlayed) return;
            if (_root == null) return;
            if (_playableDirector == null) return;

            _hasPlayed = true;

            _root.SetActive(true);
            await UniTask.WaitUntil(() => _playableDirector.time >= _playableDirector.duration, cancellationToken: ct);
            _root.SetActive(false);
        }
    }
}