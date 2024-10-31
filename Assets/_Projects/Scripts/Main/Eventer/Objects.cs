using System;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Objects
    {
        [SerializeField, Header("小道の大きな植物")]
        private Collider _bigIvyCollider;

        public void DeactivateBigIvy()
        {
            if (_bigIvyCollider == null) return;
            _bigIvyCollider.gameObject.SetActive(false);
        }
    }
}