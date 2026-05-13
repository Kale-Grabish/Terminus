using Player;
using UnityEngine;
using Weapons;

namespace AnimationEvents
{
    public class EnemyAttackDamageFrame_AnimationEvents: StateMachineBehaviour
    {
        private PlayerCombat _playerCombat;
        private WeaponBase _weapon;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_playerCombat)
            {
                _playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            }
            _weapon = _playerCombat.CurrentWeapon;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerCombat.ActivateDamage(
                stateInfo.normalizedTime >= _weapon.StartDamageFrame && 
                stateInfo.normalizedTime <= _weapon.EndDamageFrame
            );
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //set these here in case the animation is interrupted by something
            _playerCombat.Attacking = false;
            _playerCombat.ActivateDamage(false);
        }
    }
}
