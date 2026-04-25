using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponSwing : MonoBehaviour
{
    InputAction attackAction;
    [SerializeField] private GameObject Weapon;




    void Start()
    {
       

        attackAction = InputSystem.actions.FindAction("Attack");
        attackAction.performed += _ => DoAttack();
    }

    void DoAttack()
    {
        
            StartCoroutine(WeaponSwingAnimation());
        
    }
    IEnumerator WeaponSwingAnimation()
    {
        Weapon.GetComponent<Animator>().Play("WeaponSwing");
        yield return new WaitForSeconds(1.0f);
        Weapon.GetComponent<Animator>().Play("New State");
    }
}
