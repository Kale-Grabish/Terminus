using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerCombat playerCombat;
        [SerializeField] private List<WeaponBase> weapons;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject leftHand;
        
        private int _currentWeapon;
        private WeaponBase _activeWeaponInstance;
        private bool _activelySwapping;
        
        private InputAction _nextWeaponAction;
        private InputAction _prevWeaponAction;

        private void Awake()
        {
            _nextWeaponAction = InputSystem.actions.FindAction("NextWeapon");
            _nextWeaponAction.performed += _ => NextWeapon();
            
            _prevWeaponAction = InputSystem.actions.FindAction("PreviousWeapon");
            _prevWeaponAction.performed += _ => PrevWeapon();
        }

        public WeaponBase CurrentWeaponPrefab => weapons[_currentWeapon];
        public WeaponBase CurrentWeapon => _activeWeaponInstance;
        
        private void Start()
        {
            DoAnimation();
        }
        
        private void NextWeapon()
        {
            if (_activelySwapping || playerCombat.Attacking) return;
            _activelySwapping = true;
            _currentWeapon = (_currentWeapon == (weapons.Count-1)) ? 0 : _currentWeapon+1;
            DoAnimation();
        }

        private void PrevWeapon()
        {
            if (_activelySwapping || playerCombat.Attacking) return;
            _activelySwapping = true;
            _currentWeapon = _currentWeapon == 0 ? weapons.Count-1 : _currentWeapon-1;
            DoAnimation();
        }

        private void DoAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger("SwapWeapon");
            }
        }
        
        public void EnableCurrentWeapon()
        {
            _activelySwapping = false;
            if(_activeWeaponInstance?.gameObject != null) Destroy(_activeWeaponInstance.gameObject);
            _activeWeaponInstance = Instantiate(CurrentWeaponPrefab, rightHand.transform);
            _activeWeaponInstance.PositionInHands(rightHand, leftHand);
            
            // set value in animator used to determine correct idle animation
            // base on CurrentWeapon.WeaponType
            if (animator != null)
            {
                switch (CurrentWeapon.WeaponType)
                {
                    case WeaponTypeEnum.TwoHanded:
                        animator.SetFloat("WeaponType", 1.0f);
                        break;
                    case WeaponTypeEnum.OneHanded:
                    case WeaponTypeEnum.Unarmed:
                    default:
                        animator.SetFloat("WeaponType", 0.0f);
                        break;
                }
            }
            
        }
        
    }
}
