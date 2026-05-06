using Interfaces;
using Player;
using UnityEngine;

namespace Enemy
{
    public class TargetDummy : MonoBehaviour, IDamageable
    {
        
        public void TakeDamage(int damage, PainTypes painType = PainTypes.Hit)
        {
            Debug.Log("BOING!");
            
        }
    
        
        
    }
}
