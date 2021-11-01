using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LigaGame.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerActions _playerActions;
        private InputAction  _movementAction;
        private InputAction _jumpAction;
        private IPlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerActions = new PlayerActions();
        }

        private void OnEnable()
        {
            _movementAction = _playerActions.PlayerControls.Movement;
            EnableInputAction(_movementAction, MovementAction);

            _jumpAction = _playerActions.PlayerControls.Jump;
            EnableInputAction(_jumpAction, JumpAction);
        }

        private void EnableInputAction(InputAction action, Action<InputAction.CallbackContext> callback)
        {
            action.Enable();
            action.started += callback;
            action.performed += callback;
            action.canceled += callback;
            action.Enable();
        }

        private void MovementAction(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();
            _playerController.Move(direction);
        }

        private void JumpAction(InputAction.CallbackContext context)
        {
            // bool jump = context.started;
            
            // if (jump)
            // {
                _playerController.Jump();
            // }
        }
    }
}
