using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private GameObject Weapon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject Weapon = GameObject.FindWithTag("Weapon");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
     {
      
        Weapon.GetComponent<WeaponBase>().weaponAttack();
         print("weapon hit");

     }
}

