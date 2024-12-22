using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Eventer.Objects
{
    [Serializable]
    public sealed class DeersClass
    {
        [SerializeField, Required, SceneObjectsOnly, Tooltip("最初は重力をオフにしておくこと")]
        private Rigidbody _fallDeer;

        [SerializeField, Required, SceneObjectsOnly]
        private ParticleSystem _injuredDeer;

        [SerializeField, Required, SceneObjectsOnly, Tooltip("血のコライダー")]
        private Collider _injuredDeerBloodCollider;

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

            Vector3 forceDirection = _fallDeer.transform.forward + _fallDeer.transform.right;
            _fallDeer.AddForce(forceDirection * 0.1f, ForceMode.Impulse);
        }

        public void HurtByKnife()
        {
            if (_injuredDeer == null) return;
            if (_injuredDeerBloodCollider == null) return;
            if (_hasBeenHurtByKnife is true) return;

            _hasBeenHurtByKnife = true;
            _injuredDeer.Play();
            _injuredDeerBloodCollider.enabled = true;
        }
    }
}