using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private List<WeaponBase> weapons;
        private int _currentWeapon; 
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
        
        private void NextWeapon()
        {
            _currentWeapon = (_currentWeapon == (weapons.Count-1)) ? 0 : _currentWeapon+1;
            EnableCurrentWeapon();
        }

        private void PrevWeapon()
        {
            _currentWeapon = _currentWeapon == 0 ? weapons.Count-1 : _currentWeapon-1;
            EnableCurrentWeapon();
        }

        private void EnableCurrentWeapon()
        {
            Debug.Log(CurrentWeapon.WeaponName + " : " + CurrentWeapon.WeaponType);
        }
        
    }
}
