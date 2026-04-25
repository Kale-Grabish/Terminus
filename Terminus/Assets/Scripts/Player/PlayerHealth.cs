using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] 
        private int maxHealth = 25;
        private int _currentHealth;
        
        private void Start()
        {
            CurrentHealth = maxHealth;
        }
        
        public int MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, maxHealth);
                DeathCheck();
            }
        }

        private void DeathCheck()
        {
            if (_currentHealth <= 0)
            {
                //game over or respawn
            }            
        }
        
        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
        }
        
        public void Heal(int amount)
        {
            CurrentHealth += amount;
        }
        
    }
}
