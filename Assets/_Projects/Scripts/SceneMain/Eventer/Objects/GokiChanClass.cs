using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class GokiChanClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Route[] _routes;

        [SerializeField, Required, SceneObjectsOnly]
        private Transform _transform;

        private int _currentIndex = 0;

        public async UniTask TraceNext(CancellationToken ct)
        {
            if (_transform == null) return;
            if (_routes is null) return;
            if (_routes.Length <= 0) return;
            if (_currentIndex >= _routes.Length) return;

            Route route = _routes[_currentIndex++];
            if (route is null) return;
            await route.Trace(_transform, ct);
        }

        [Serializable]
        private sealed class Route
        {
            [SerializeField, Required, SceneObjectsOnly, Tooltip("最初から順に辿る(y座標も考慮、曲線で繋ぐ)")]
            private Transform[] _route;

            [SerializeField, Range(0.1f, 30.0f), Tooltip("移動時間")]
            private float _duration;

            [SerializeField, Tooltip("辿り終わったら非表示にするか")]
            private bool _isDeactivateOnEnd;

            private static readonly PathType PathType = PathType.CatmullRom;
            private static readonly PathMode PathMode = PathMode.Full3D;

            // ワールド座標
            private Vector3[] _positions = null;
            private Vector3[] positions
            {
                get
                {
                    if (_positions is null)
                    {
                        try
                        {
                            int len = _route.Length;
                            _positions = new Vector3[len];
                            for (int i = 0; i < len; i++)
                            {
                                _positions[i] = _route[i].position;
                            }
                        }
                        catch (Exception) { _positions = null; }
                    }

                    return _positions;
                }
            }

            public async UniTask Trace(Transform agent, CancellationToken ct)
            {
                if (agent == null) return;
                if (positions is null) return;
                if (positions.Length <= 0) return;

                agent.position = positions[0];
                if (agent.gameObject.activeSelf is false) agent.gameObject.SetActive(true);

                await agent.DOPath(positions, _duration, PathType, PathMode).ToUniTask(cancellationToken: ct);
                if (_isDeactivateOnEnd) agent.gameObject.SetActive(false);
            }
        }
    }
}