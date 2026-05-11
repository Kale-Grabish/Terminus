using UnityEngine;
using Interfaces;
using Player;

namespace Enemy
{
    public class EnemyWeapon :  MonoBehaviour
    {
        [SerializeField] private int attackDamage;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private PainTypes painType = PainTypes.Hit;
        
        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log("hit");
            if (!other.CompareTag("Damageable") && !other.CompareTag("Player")) return;
            other.GetComponent<IDamageable>().TakeDamage(attackDamage, painType);
            if (!impactEffect) return;
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }
}