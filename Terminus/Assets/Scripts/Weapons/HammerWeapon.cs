using UnityEngine;

namespace Weapons
{
    public class HammerWeapon: WeaponBase
    {
        
        public override void PositionInHands(GameObject rightHand, GameObject leftHand)
        {
            transform.localRotation = Quaternion.Euler(0, -150, -90);
            transform.localPosition = new Vector3(0.15f, 0.07f, 0.0f);
        }
        
        public override void PoseForAttack(bool forAttack)
        {
            if (forAttack)
            {
                transform.localRotation = Quaternion.Euler(0, -180, -90);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, -150, -90);
            }
        }
    }
}

// -180, 10, 70