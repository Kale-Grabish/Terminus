using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput: MonoBehaviour
    {
        [SerializeField] private InputActionAsset playerControls;
         
        private InputAction _moveAction;
        private InputAction _attackAction;
        private InputAction _dodgeAction;
        private InputAction _jumpAction;
        
        public Vector2 MoveVector { get; private set; }
        public bool Attacking { get; private set; }
        public bool Dodge { get; private set; }
        public bool Jump {  get; private set; } 

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _moveAction = playerControls.FindActionMap("Player").FindAction("Move");
            _moveAction.performed += ctx => MoveVector = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => MoveVector = Vector2.zero;

            _attackAction = playerControls.FindActionMap("Player").FindAction("Attack");
            _attackAction.performed += _ => Attacking = true;
            _attackAction.canceled += _ => Attacking = false;

            _dodgeAction = playerControls.FindActionMap("Player").FindAction("Dodge");
            _dodgeAction.started += _ => Dodge = true;
            _dodgeAction.canceled += _ => Dodge = false;

            _jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");
            _jumpAction.started += _ => Jump = true;
            _jumpAction.performed += _ => Jump = false;
            
        }
    }
}