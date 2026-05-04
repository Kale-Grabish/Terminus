using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private int attackDamage = 2;
        [SerializeField] private string weaponName = "unnamed";
        [SerializeField] private WeaponTypeEnum weaponType;

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
    }
}