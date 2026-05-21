using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private InputAction _attackAction;
        private PlayerMovement _playerMovement;
        private PlayerInventory _playerInventory;

        [SerializeField] private Animator animator;
        public bool Attacking { get; set; }
        public WeaponBase CurrentWeapon => _playerInventory.CurrentWeapon; 
        
        
        private void Awake()
        {
            _attackAction = InputSystem.actions.FindAction("Attack");
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInventory = GetComponent<PlayerInventory>();
            
            _attackAction.performed += _ => StartAttack();
        }

        private bool CanAttack()
        {
            return !Attacking && _playerMovement.CurrentMovementState != PlayerMovementStates.Dodge;
        }
        
        public void ActivateDamage(bool activate)
        {
            _playerInventory.CurrentWeapon.ActivateDamage(activate);            
        }
        
        private void StartAttack()
        {
            if (!CanAttack()) return;
            Attacking = true;
            if (animator != null)
            {
                switch (CurrentWeapon.WeaponType)
                {
                    case WeaponTypeEnum.OneHanded:
                        animator.SetTrigger("DoOneSwing");
                        break;
                    case WeaponTypeEnum.TwoHanded:
                        animator.SetTrigger("DoTwoSwing");
                        break;
                }
            }
        }
    }
}