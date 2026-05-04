using UnityEngine;
using Player;

namespace AnimationEvents
{
    // this makes the call to change the players weapon once a specified time passes in this animation
    public class WeaponSwapAnimationEvents : StateMachineBehaviour
    {
        private PlayerInventory _playerInventory;
        private bool _triggered;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // we just entered this animation state so get reference to the players
            // inventory & set trigger off
            if (!_playerInventory)
            {
                _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();    
            }
            _triggered = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // if already triggered or not enough time has passed then bail
            if (_triggered || stateInfo.normalizedTime <= 0.49) return;
            // otherwise call function to enable weapon & set trigger on
            _playerInventory.EnableCurrentWeapon();
            _triggered = true;
        }
    }
}
