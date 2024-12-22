using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class GlassPieceWayClass
    {
        [SerializeField, Required, SceneObjectsOnly]
        private Collider _collider;

        private bool _isScatteredGlassPiece = false;

        public bool IsScatteredGlassPiece
        {
            get => _isScatteredGlassPiece;
            set => _isScatteredGlassPiece = value;
        }
    }
}