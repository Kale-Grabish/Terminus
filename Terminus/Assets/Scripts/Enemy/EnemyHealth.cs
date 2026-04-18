using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float hitPoints = 10f;
    public float minHP = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
