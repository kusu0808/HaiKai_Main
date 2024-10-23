using BorderSystem;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Main.EventManager
{
    [Serializable]
    public sealed class Borders
    {
        private ReadOnlyCollection<Border> _busStopCannotMoveRc = null;
        [SerializeField, Tooltip("バス停：ここから先には行けない！")]
        private Border[] _busStopCannotMove;
        public ReadOnlyCollection<Border> BusStopCannotMove
        { get { _busStopCannotMoveRc ??= Array.AsReadOnly(_busStopCannotMove); return _busStopCannotMoveRc; } }
        public bool IsInAnyBusStopCannotMove(Vector3 pos)
        {
            foreach (var border in _busStopCannotMove) if (border.IsIn(pos) is true) return true;
            return false;
        }

        [SerializeField, Tooltip("橋：きしむ音を立てる")]
        private Border _bridgePlaySound;
        public Border BridgePlaySound => _bridgePlaySound;
    }
}