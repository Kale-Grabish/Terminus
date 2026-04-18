using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 25;
        public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0);
            //game over or respawn
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
