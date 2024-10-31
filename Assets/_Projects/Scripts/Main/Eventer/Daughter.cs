using System;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Daughter
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField, Header("小道に散らばる予定の、娘の持ち物全て")]
        private GameObject[] _pathWayItems;

        public bool IsActive
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

        public void SetPathWayItemsEnabled(bool value)
        {
            if (_pathWayItems is null) return;

            foreach (GameObject item in _pathWayItems)
            {
                if (item == null) continue;
                item.SetActive(value);
            }
        }
    }
}