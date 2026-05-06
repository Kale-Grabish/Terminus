using Interfaces;
using Player;
using UnityEngine;

namespace Enemy
{
    public class TargetDummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator animator;
        
        public void TakeDamage(int damage, PainTypes painType = PainTypes.Hit)
        {
            if (animator != null)
            {
                animator.SetTrigger("GotHit");
            }
        }
    
        
        
    }
}
