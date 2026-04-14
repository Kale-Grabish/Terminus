using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBase : MonoBehaviour
{
    public float hitPoints = 10f;
    public float minHP = 0f;
    private float timer = 0.0f;
    private float delayTime = 5.0f;
    InputAction attackAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
       
    }
    
    // Update is called once per frame
    void Update()
    {
        

    }



    private void OnCollisionStay(Collision collision)
    {
        print("collision active");

     
        weaponAttack();

        timer += Time.deltaTime;
        if (timer < delayTime)
        {
            print("delay active");
        }

        if (timer > delayTime)
        {
            print("delay reset");
            timer = 0.0f;
        }
    }
    
    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
      attackAction.Enable();
    }

    private void weaponAttack()
    
        {
        if (attackAction.IsPressed())
        {
            
            print("weapon press");
            print(hitPoints);
            StartCoroutine(DelayAction(delayTime));
            if (hitPoints > minHP)
            {
                
                hitPoints--;
                if (timer < delayTime)
                {
                    attackAction.Disable();
                    print("attack disabled");
                }
            }

        }
    }
  }