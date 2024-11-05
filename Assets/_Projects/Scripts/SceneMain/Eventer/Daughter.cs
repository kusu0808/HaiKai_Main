using System;
using UnityEngine;

namespace Main.Eventer
{
    [Serializable]
    public sealed class Daughter
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField, Header("ナイフ(小道に散らばる予定)")]
        private GameObject _knife;

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

        // 一括変更
        public void SetPathWayItemsEnabled(bool value)
        {
            if (_knife != null) _knife.SetActive(value);
        }

        // 個別変更(一括変更の方と競合し得る)
        public void SetKnifeEnabled(bool value)
        {
            if (_knife != null) _knife.SetActive(value);
        }
    }
}