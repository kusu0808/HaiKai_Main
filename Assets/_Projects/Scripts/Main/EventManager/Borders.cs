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
        public bool IsInAny(Vector3 pos)
        {
            foreach (var border in _busStopCannotMove) if (border.IsIn(pos) is true) return true;
            return false;
        }
    }
}