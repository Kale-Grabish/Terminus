using UnityEngine;
using Player;

namespace AnimationEvents
{
    public class WeaponSwapAnimationEvents : StateMachineBehaviour
    {

        private PlayerInventory _playerInventory;
        private bool _triggered;

        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_playerInventory)
            {
                _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();    
            }
            _triggered = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_triggered || stateInfo.normalizedTime <= 0.49) return;
            _playerInventory.EnableCurrentWeapon();
            _triggered = true;
        }
        

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

    }
}
