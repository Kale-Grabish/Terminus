using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private Animator _animator;
        private PlayerCombat _playerCombat;
        [SerializeField] private List<WeaponBase> weapons;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject leftHand;
        
        private int _currentWeapon;
        private bool _activelySwapping;
        
        private InputAction _nextWeaponAction;
        private InputAction _prevWeaponAction;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerCombat = GetComponent<PlayerCombat>();

            _nextWeaponAction = InputSystem.actions.FindAction("NextWeapon");
            _nextWeaponAction.performed += _ => NextWeapon();
            
            _prevWeaponAction = InputSystem.actions.FindAction("PreviousWeapon");
            _prevWeaponAction.performed += _ => PrevWeapon();
        }

        private WeaponBase CurrentWeaponPrefab => weapons[_currentWeapon];
        public WeaponBase CurrentWeapon { get; private set; }

        private void Start()
        {
            DoAnimation();
        }
        
        private void NextWeapon()
        {
            if (_activelySwapping || _playerCombat.Attacking) return;
            _activelySwapping = true;
            _currentWeapon = (_currentWeapon == (weapons.Count-1)) ? 0 : _currentWeapon+1;
            DoAnimation();
        }

        private void PrevWeapon()
        {
            if (_activelySwapping || _playerCombat.Attacking) return;
            _activelySwapping = true;
            _currentWeapon = _currentWeapon == 0 ? weapons.Count-1 : _currentWeapon-1;
            DoAnimation();
        }

        private void DoAnimation()
        {
            if (_animator != null)
            {
                _animator.SetTrigger("SwapWeapon");
            }
        }
        
        public void EnableCurrentWeapon()
        {
            _activelySwapping = false;
            if(CurrentWeapon?.gameObject != null) Destroy(CurrentWeapon.gameObject);
            CurrentWeapon = Instantiate(CurrentWeaponPrefab, rightHand.transform);
            CurrentWeapon.PositionInHands(rightHand, leftHand);
            
            // set value in animator used to determine correct idle animation
            // base on CurrentWeapon.WeaponType
            if (_animator != null)
            {
                switch (CurrentWeapon.WeaponType)
                {
                    case WeaponTypeEnum.TwoHanded:
                        _animator.SetFloat("WeaponType", 1.0f);
                        break;
                    case WeaponTypeEnum.OneHanded:
                    case WeaponTypeEnum.Unarmed:
                    default:
                        _animator.SetFloat("WeaponType", 0.0f);
                        break;
                }
            }
            
        }
        
    }
}
