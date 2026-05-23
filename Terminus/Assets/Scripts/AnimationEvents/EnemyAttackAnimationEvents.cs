using Enemy;
using UnityEngine;

namespace AnimationEvents
{
    public class EnemyAttackAnimationEvents: StateMachineBehaviour
    {
        private EnemyWeapon _weapon;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_weapon)
            {
                _weapon = animator.gameObject.GetComponentInChildren<EnemyWeapon>();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _weapon.DamageActive = 
                stateInfo.normalizedTime >= _weapon.StartDamageFrame && 
                stateInfo.normalizedTime <= _weapon.EndDamageFrame
            ;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //set these here in case the animation is interrupted by something
            _weapon.DamageActive = false;
        }
    }
}
