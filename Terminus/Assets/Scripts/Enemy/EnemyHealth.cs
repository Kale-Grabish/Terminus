using Interfaces;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {

        [SerializeField] public int maxHealth;
        private int _currentHealth;
        
        private void Start()
        {
            InitHealth();
        }

        private void InitHealth()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, PainTypes painType = PainTypes.Hit)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)Die();
        }
    
        private void Die()
        {
            Destroy(gameObject);
        }
        
    }
}
