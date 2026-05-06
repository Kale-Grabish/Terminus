using UnityEngine;
using Enemy;
using Interfaces;
using Player;

namespace Weapons
{
    public enum WeaponTypeEnum
    {
        OneHanded,
        TwoHanded,
        Unarmed
    }
    
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected int attackDamage = 2;
        [SerializeField] protected string weaponName = "unnamed";
        [SerializeField] protected WeaponTypeEnum weaponType;
        [SerializeField] protected PainTypes painType;
        [SerializeField] protected float startDamageFrame;
        [SerializeField] protected float endDamageFrame;
        [SerializeField] protected BoxCollider damageArea;
        [SerializeField] protected GameObject impactEffect;
        
        public float StartDamageFrame => startDamageFrame;
        public float EndDamageFrame => endDamageFrame;
        
        public int AttackDamage
        {
            get => attackDamage;
            private set => attackDamage = value;
        }

        public string WeaponName
        {
            get => weaponName;
            private set => weaponName = value;
        }

        public WeaponTypeEnum WeaponType
        {
            get => weaponType;
            private set => weaponType = value;
        }

        public virtual void PositionInHands(GameObject rightHand, GameObject leftHand)
        {

        }

        public void Start()
        {
            damageArea.enabled = false;
        }

        public virtual void ActivateDamage(bool activate)
        {
            damageArea.enabled = activate;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Damageable"))
            {
                other.GetComponent<IDamageable>().TakeDamage(attackDamage, painType);
                if (impactEffect)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
            }
        }
        
    }
}