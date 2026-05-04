using UnityEngine;

namespace Weapons
{
    public class BarMaceWeapon: WeaponBase
    {
        [SerializeField] private GameObject rightHandGrip;
        [SerializeField] private GameObject leftHandGrip;
        
        public override void PositionInHands(GameObject rightHand, GameObject leftHand)
        {
            transform.localPosition = new Vector3(0.0f, 0.05f, 0.0f);
            transform.localRotation = Quaternion.Euler(-90, 0, 90);
            
        }
    }
}