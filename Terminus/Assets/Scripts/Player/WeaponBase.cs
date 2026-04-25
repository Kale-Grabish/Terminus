using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    // placeholder until health system is implemented
    public float hitPoints = 50f;
    public float minHP = 1f;
    public int attackDamage = 2;
    private float timer = 0.0f;
    private float attackSpeed = 1.0f; //the lower the number set in this, the less time between weapon swings
    InputAction attackAction;
    CharacterController Player;



    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");

    }

    private void OnTriggerStay(Collider other)
    {
        // looks for game objects with the enemy tag. If found, runs weapon attack and weapon timer

        // print("collision active");
         if (other.CompareTag("Enemy"))
            {
            weaponAttack();
            weaponSpeed();
        }
    }
    
    public void weaponAttack()
    
        {

        
        if (attackAction.IsPressed())
        {
           
            print(hitPoints);

            StartCoroutine(WeaponTimer(attackSpeed));
           
            if (hitPoints > minHP)
            {
                
                hitPoints = hitPoints - attackDamage;
                if (timer < attackSpeed)
                {
                    attackAction.Disable();
                }
            }
            else
            {
                GameObject enemy = GameObject.FindWithTag("Enemy");
                enemy.GetComponent<EnemyHealth>().Die();
            }

        }
    }

    void weaponSpeed()
    {
        // timer for weapon swings
        timer += Time.deltaTime;

        if (timer > attackSpeed)
        {
            timer = 0.0f;
        }
    }
    IEnumerator WeaponTimer(float attackSpeed)
    {

        yield return new WaitForSeconds(attackSpeed);
        attackAction.Enable();
    }

}