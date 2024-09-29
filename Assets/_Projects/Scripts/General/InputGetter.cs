using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IA
{
    #region

    public enum InputType
    {
        /// <summary>
        /// 【null】デフォルト値。何も意味しない
        /// </summary>
        Null,

        /// <summary>
        /// 【bool】そのフレームが、押された瞬間のフレームであるか
        /// </summary>
        Click,

        /// <summary>
        /// 【bool】そのフレームが、一定秒数押された瞬間のフレームであるか
        /// </summary>
        Hold,

        /// <summary>
        /// 【bool】そのフレームにおける、押されているかのフラグ
        /// </summary>
        Value0,

        /// <summary>
        /// 【float】そのフレームにおける、1軸の入力の値(単位線 以内)
        /// </summary>
        Value1,

        /// <summary>
        /// 【Vector2】そのフレームにおける、2軸の入力の値(単位円 以内)
        /// </summary>
        Value2,

        /// <summary>
        /// 【Vector3】そのフレームにおける、3軸の入力の値(単位球 以内)
        /// </summary>
        Value3
    }

    public sealed class InputInfo : IDisposable
    {
        private InputAction _inputAction;
        private readonly InputType _type;
        private ReadOnlyCollection<Action<InputAction.CallbackContext>> _action;

        public bool Bool { get; private set; } = false;
        public float Float { get; private set; } = 0;
        public Vector2 Vector2 { get; private set; } = Vector2.zero;
        public Vector3 Vector3 { get; private set; } = Vector3.zero;

        public InputInfo(InputAction inputAction, InputType type)
        {
            this._inputAction = inputAction;
            this._type = type;

            this._action = this._type switch
            {
                InputType.Null => null,

                InputType.Click => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; }
                }
                .AsReadOnly(),

                InputType.Hold => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; }
                }
                .AsReadOnly(),

                InputType.Value0 => new List<Action<InputAction.CallbackContext>>()
                {
                    _ => { Bool = true; },
                    _ => { Bool = false; }
                }
                .AsReadOnly(),

                InputType.Value1 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Float = e.ReadValue<float>(); }
                }
                .AsReadOnly(),

                InputType.Value2 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Vector2 = e.ReadValue<Vector2>(); }
                }
                .AsReadOnly(),

                InputType.Value3 => new List<Action<InputAction.CallbackContext>>()
                {
                    e => { Vector3 = e.ReadValue<Vector3>(); }
                }
                .AsReadOnly(),

                _ => null
            };
        }

        public void Dispose()
        {
            _inputAction = null;
            _action = null;
        }

        public void Link(bool isLink)
        {
            if (_inputAction == null) return;
            if (_action == null) return;

            if (isLink)
            {
                switch (_type)
                {
                    case InputType.Null:
                        break;

                    case InputType.Click:
                        _inputAction.performed += _action[0];
                        break;

                    case InputType.Hold:
                        _inputAction.performed += _action[0];
                        break;

                    case InputType.Value0:
                        _inputAction.performed += _action[0];
                        _inputAction.canceled += _action[1];
                        break;

                    case InputType.Value1:
                        _inputAction.started += _action[0];
                        _inputAction.performed += _action[0];
                        _inputAction.canceled += _action[0];
                        break;

                    case InputType.Value2:
                        _inputAction.started += _action[0];
                        _inputAction.performed += _action[0];
                        _inputAction.canceled += _action[0];
                        break;

                    case InputType.Value3:
                        _inputAction.started += _action[0];
                        _inputAction.performed += _action[0];
                        _inputAction.canceled += _action[0];
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (_type)
                {
                    case InputType.Null:
                        break;

                    case InputType.Click:
                        _inputAction.performed -= _action[0];
                        break;

                    case InputType.Hold:
                        _inputAction.performed -= _action[0];
                        break;

                    case InputType.Value0:
                        _inputAction.performed -= _action[0];
                        _inputAction.canceled -= _action[1];
                        break;

                    case InputType.Value1:
                        _inputAction.started -= _action[0];
                        _inputAction.performed -= _action[0];
                        _inputAction.canceled -= _action[0];
                        break;

                    case InputType.Value2:
                        _inputAction.started -= _action[0];
                        _inputAction.performed -= _action[0];
                        _inputAction.canceled -= _action[0];
                        break;

                    case InputType.Value3:
                        _inputAction.started -= _action[0];
                        _inputAction.performed -= _action[0];
                        _inputAction.canceled -= _action[0];
                        break;

                    default:
                        break;
                }
            }
        }

        public void OnLateUpdate()
        {
            if (_type == InputType.Click && Bool) Bool = false;
            else if (_type == InputType.Hold && Bool) Bool = false;
        }
    }

    public static class InputEx
    {
        public static InputInfo Add(this InputInfo inputInfo, List<InputInfo> list)
        {
            list.Add(inputInfo);
            return inputInfo;
        }
    }

    #endregion

    public sealed class InputGetter : MonoBehaviour
    {
        #region

        private IA _ia;
        private List<InputInfo> _inputInfoList;
        public static InputGetter Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            _ia = new();
            _inputInfoList = new();

            Init();

            foreach (InputInfo e in _inputInfoList) e.Link(true);
        }
        private void OnDestroy()
        {
            foreach (InputInfo e in _inputInfoList) e.Link(false);

            _ia.Dispose();
            foreach (InputInfo e in _inputInfoList) e.Dispose();

            _ia = null;
            _inputInfoList = null;
        }

        private void OnEnable() => _ia.Enable();
        private void OnDisable() => _ia.Disable();

        private void LateUpdate()
        {
            foreach (InputInfo e in _inputInfoList) e.OnLateUpdate();
        }

        #endregion

        public InputInfo PlayerMove { get; private set; }
        public InputInfo PlayerLook { get; private set; }
        public InputInfo PlayerInteract { get; private set; }
        public InputInfo Submit { get; private set; }
        public InputInfo Cancel { get; private set; }
        public InputInfo Select { get; private set; }

        private void Init()
        {
            PlayerMove = new InputInfo(_ia.Player.Move, InputType.Value2).Add(_inputInfoList);
            PlayerLook = new InputInfo(_ia.Player.Look, InputType.Value2).Add(_inputInfoList);
            PlayerInteract = new InputInfo(_ia.Player.Interact, InputType.Click).Add(_inputInfoList);
            Submit = new InputInfo(_ia.General.Submit, InputType.Click).Add(_inputInfoList);
            Cancel = new InputInfo(_ia.General.Cancel, InputType.Click).Add(_inputInfoList);
            Select = new InputInfo(_ia.General.Select, InputType.Value1).Add(_inputInfoList);
        }
    }
}