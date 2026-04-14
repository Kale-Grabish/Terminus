using UnityEditor.Timeline.Actions;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class WeaponSwing : MonoBehaviour
{
    InputAction attackAction;
    public GameObject Weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");

    }

    // Update is called once per frame
    void Update()
    {
        if(attackAction.IsPressed())
        {
            StartCoroutine(WeaponSwingAnimation());
        }
    }
    IEnumerator WeaponSwingAnimation()
    {
        Weapon.GetComponent<Animator>().Play("WeaponSwing");
        yield return new WaitForSeconds(1.0f);
        Weapon.GetComponent<Animator>().Play("New State");
    }
}
