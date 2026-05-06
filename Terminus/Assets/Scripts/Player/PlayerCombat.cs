using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private InputAction _attackAction;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerInventory playerInventory;

        [SerializeField] private Animator animator;
        public bool Attacking { get; set; }
        public WeaponBase CurrentWeapon => playerInventory.CurrentWeapon; 
        
        
        private void Awake()
        {
            _attackAction = InputSystem.actions.FindAction("Attack");
            _attackAction.performed += _ => StartAttack();
        }

        private bool CanAttack()
        {
            return !Attacking && playerMovement.CurrentMovementState != PlayerMovementStates.Dodge;
        }
        
        public void ActivateDamage(bool activate)
        {
            playerInventory.CurrentWeapon.ActivateDamage(activate);            
        }
        
        private void StartAttack()
        {
            if (!CanAttack()) return;
            Attacking = true;
            if (animator != null)
            {
                animator.SetTrigger("MakeMeleeAttack");
            }
        }
    }
}