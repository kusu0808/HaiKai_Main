using System;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Daughter
    {
        [SerializeField]
        private Transform _transform;

        public bool isActive
        {
            get
            {
                if (_transform == null) return false;
                return _transform.gameObject.activeSelf;
            }
            set
            {
                if (_transform == null) return;
                _transform.gameObject.SetActive(value);
            }
        }

        // ワールド
        public Vector3 Position
        {
            get
            {
                if (_transform == null) return Vector3.zero;
                return _transform.position;
            }
            set
            {
                if (_transform == null) return;
                _transform.position = value;
            }
        }
    }
}