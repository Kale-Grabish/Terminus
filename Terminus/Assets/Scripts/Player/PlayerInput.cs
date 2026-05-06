using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput: MonoBehaviour
    {
        [SerializeField] private InputActionAsset playerControls;
         
        private InputAction _moveAction;
        private InputAction _dodgeAction;
        
        public Vector2 MoveVector { get; private set; }
        public bool Dodge { get; private set; }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _moveAction = playerControls.FindActionMap("Player").FindAction("Move");
            _moveAction.performed += ctx => MoveVector = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => MoveVector = Vector2.zero;
            
            _dodgeAction = playerControls.FindActionMap("Player").FindAction("Dodge");
            _dodgeAction.started += _ => Dodge = true;
            _dodgeAction.canceled += _ => Dodge = false;
        }
    }
}