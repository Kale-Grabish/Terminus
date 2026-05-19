using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boomerang : MonoBehaviour
{
    [SerializeField] GameObject boomerang;
    [SerializeField] Transform boomerangLocation;
    [SerializeField] Transform boomerangRotation;
    [SerializeField] float boomerangDistance;
    [SerializeField] float throwSpeed;
    [SerializeField] LayerMask layerMask;

    [SerializeField] bool isThrown;
    [SerializeField] bool isReturning;

    [SerializeField] Vector3 throwPosition;
    [SerializeField] Vector3 returnPosition;
    [SerializeField] Rotator rotator;
    [SerializeField] private Player.PlayerInput playerInput;
    [SerializeField] Camera playerCamera;


    // Update is called once per frame
    void Update()
    {
        if (playerInput.Throw)
        {
            //If isthrown or isReturning is true, go away, else check distance
            if (isThrown || isReturning) return;
            CheckDistance();
        }

        if (isThrown)
        {
            Vector3 newPos = Vector3.MoveTowards(boomerang.transform.position, throwPosition, throwSpeed * Time.deltaTime);
            boomerang.transform.position = newPos;
            boomerang.GetComponent<MeshCollider>().enabled = true; 

            //if the boomerangs position is equal to the throw position
            if(boomerang.transform.position == throwPosition)
            {
                isThrown = false;
                isReturning = true;
            }
        }

        if (isReturning)
        {
            //Set the new position back to the boomerangs original postion
            Vector3 newPos = Vector3.MoveTowards(boomerang.transform.position, boomerangLocation.position, throwSpeed * Time.deltaTime);
            boomerang.transform.position = newPos;

            //if Boomerangs original position is equal to original location
            if (boomerang.transform.position == boomerangLocation.position)
            {
                //Set isReturning to false, turn off the rotator, set parent and rotation
                isReturning = false;
                rotator.enabled = false;
                boomerang.transform.parent = boomerangLocation;
                boomerang.transform.rotation = boomerangRotation.rotation;
            }
        }
    }

    void CheckDistance()
    {
        RaycastHit hit;

        
        if (Physics.Raycast(boomerangLocation.transform.position, playerCamera.transform.forward, out hit, boomerangDistance,layerMask))
        {
            throwPosition = hit.point;
            boomerang.transform.parent = null;
            rotator.enabled = true;
            isThrown = true;
        }

        else
        {
            throwPosition = boomerangLocation.position + playerCamera.transform.forward * boomerangDistance;
            boomerang.transform.parent = null;
            returnPosition = boomerangLocation.position;
            rotator.enabled = true;
            isThrown = true;
        }
    }
}
