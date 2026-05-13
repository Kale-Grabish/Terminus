using System;
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
        [SerializeField] private float startDamageFrame;
        [SerializeField] private float endDamageFrame;
        [SerializeField] private BasicSkeletonEnemy wielder;
        public bool DamageActive { get; set; }
        public float StartDamageFrame => startDamageFrame;
        public float EndDamageFrame => endDamageFrame;

        protected void OnTriggerEnter(Collider other)
        {
            //if wielder is dead: bail
            if (!wielder.IsAlive()) return;
            
            // if not in a damage frame (see animation events) then weapon is not current capable
            // of hitting anything; so bail
            if (!DamageActive) return;
            
            // if what we collided with is not a damageable object or player: bail
            if (!other.CompareTag("Damageable") && !other.CompareTag("Player")) return;

            // try to inflict damage
            bool didHurt = other.GetComponent<IDamageable>().TakeDamage(attackDamage, painType);

            // if damage was inflicted & we have impact FX then instantiate it
            if (didHurt && !impactEffect) return;
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }
}