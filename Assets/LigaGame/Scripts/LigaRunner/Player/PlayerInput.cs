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
            _playerController = GetComponent<IPlayerController>();
            _playerActions = new PlayerActions();
        }

        private void OnEnable()
        {
            _movementAction = _playerActions.PlayerControls.Movement;
            _movementAction.started += MovementAction;
            _movementAction.performed += MovementAction;
            _movementAction.canceled += MovementAction;
            _movementAction.Enable();

            _jumpAction = _playerActions.PlayerControls.Jump;
            _jumpAction.started += JumpAction;
            _jumpAction.Enable();
        }

        private void MovementAction(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();
            _playerController.Move(direction);
        }

        private void JumpAction(InputAction.CallbackContext context)
        {
            _playerController.Jump();
        }
    }
}
