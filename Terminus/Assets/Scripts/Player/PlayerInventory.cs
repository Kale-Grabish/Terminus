using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
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

        public WeaponBase CurrentWeapon => weapons[_currentWeapon];

        private void Start()
        {
            DoAnimation();
        }
        
        private void NextWeapon()
        {
            if (_activelySwapping) return;
            _activelySwapping = true;
            _currentWeapon = (_currentWeapon == (weapons.Count-1)) ? 0 : _currentWeapon+1;
            DoAnimation();
        }

        private void PrevWeapon()
        {
            if (_activelySwapping) return;
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
            _activeWeaponInstance = Instantiate(CurrentWeapon, rightHand.transform);
            _activeWeaponInstance.PositionInHands(rightHand, leftHand);
        }
        
    }
}
