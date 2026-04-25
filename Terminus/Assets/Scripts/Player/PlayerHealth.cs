using UI;
using UnityEngine;

namespace Player
{
    public enum PainTypes
    {
        Hit,
        HeadHurts
    }
    
    public class PlayerHealth : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int GotHitTrigger = Animator.StringToHash("gotHit");
        
        [SerializeField] private Animator animator;

        [SerializeField] private int maxHealth = 25;
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
                animator.SetBool(IsDead, true);
                GetComponentInChildren<SceneSelectorMenu>().ShowMenu(true);
            }
        }

        public void TakeDamage(int amount, PainTypes painType=PainTypes.Hit)
        {
            
            CurrentHealth -= amount;

            if (_currentHealth <= 0) return;
            
            switch (painType)
            {
                case PainTypes.Hit:
                {
                    animator.SetTrigger(GotHitTrigger);
                    break;
                }
            }
            

        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
        }
    }
}
