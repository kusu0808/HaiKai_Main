using IA;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// かなり品質の悪い作り
    /// </summary>
    public sealed class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Transform _armatureTf;
        [SerializeField] private Rigidbody _armatureRb;
        [SerializeField] private Transform _cameraTf;
        [Space(25)]
        [SerializeField] private float _moveForce;
        [SerializeField] private float _targetMoveSpeed;
        [SerializeField] private float _lookSensitivityH;
        [SerializeField] private float _lookSensitivityV;

        private const float G = 9.81f;

        private void FixedUpdate()
        {
            Vector2 moveInput = InputGetter.Instance.PlayerMove.Vector2;
            Vector3 dir = (_armatureTf.right * moveInput.x + _armatureTf.forward * moveInput.y).normalized;
            Vector3 force = dir * _moveForce;
            _armatureRb.AddForce(force);
            float k = _armatureRb.velocity.magnitude == 0 ? 0 : _targetMoveSpeed / _armatureRb.velocity.magnitude;
            _armatureRb.AddForce(-k * _armatureRb.velocity);
        }

        private void LateUpdate()
        {
            Vector2 input = InputGetter.Instance.PlayerLook.Vector2;
            Quaternion rotH = Quaternion.AngleAxis(input.x * _lookSensitivityH * Time.deltaTime, _armatureTf.up);
            _armatureTf.rotation = rotH * _armatureTf.rotation;
            Quaternion rotV = Quaternion.AngleAxis(-input.y * _lookSensitivityV * Time.deltaTime, _cameraTf.right);
            _cameraTf.rotation = rotV * _cameraTf.rotation;
        }
    }
}