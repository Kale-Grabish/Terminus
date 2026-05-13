using Interfaces;
using Player;
using UnityEngine;

namespace Enemy
{
    public class TargetDummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator animator;
        
        public bool TakeDamage(int damage, PainTypes painType = PainTypes.Hit)
        {
            if (animator)
            {
                animator.SetTrigger("GotHit");
            }
            return true;
        }

        public bool IsAlive()
        {
            return true;
        }
    }
}
