using UnityEngine;

namespace Weapons
{
    public class HammerWeapon: WeaponBase
    {
        public override void PositionInHands(GameObject rightHand, GameObject leftHand)
        {
            // transform.Rotate(-0, -90,180);
            // 0, -90, -90
            transform.localRotation = Quaternion.Euler(0, -90, -90);
            transform.localPosition = new Vector3(0.15f, 0.07f, 0.0f);
        }
    }
}