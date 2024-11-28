using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class DeersClass
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("最初は重力をオフにしておくこと")]
        private Rigidbody _fallDeer;

        [SerializeField, Required, SceneObjectsOnly]
        private ParticleSystem _injuredDeer;

        private bool _hasFalled = false;
        public bool HasFalled => _hasFalled;

        private bool _hasBeenHurtByKnife = false;
        public bool HasBeenHurtByKnife => _hasBeenHurtByKnife;

        public void Fall()
        {
            if (_fallDeer == null) return;
            if (_hasFalled is true) return;

            _hasFalled = true;
            _fallDeer.useGravity = true;
        }

        public void HurtByKnife()
        {
            if (_injuredDeer == null) return;
            if (_hasBeenHurtByKnife is true) return;

            _hasBeenHurtByKnife = true;
            _injuredDeer.Play();
        }
    }
}