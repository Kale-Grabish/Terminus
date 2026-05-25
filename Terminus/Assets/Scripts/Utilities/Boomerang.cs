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

    private float throwProgress = 0f;
    private Vector3 throwStartPosition;
    private Vector3 throwRight;
    [SerializeField] float arcHeight = 3f;


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
            throwProgress += throwSpeed * Time.deltaTime / Vector3.Distance(throwStartPosition, throwPosition);
            throwProgress = Mathf.Clamp01(throwProgress);

            // Base straight line position
            Vector3 straightPos = Vector3.Lerp(throwStartPosition, throwPosition, throwProgress);

            // Sine offset peaks in the middle, zero at start and end
            float sineOffset = Mathf.Sin(throwProgress * Mathf.PI) * arcHeight;
            boomerang.transform.position = straightPos + throwRight * sineOffset;

            boomerang.GetComponent<MeshCollider>().enabled = true;

            if (throwProgress >= 1f)
            {
                isThrown = false;
                isReturning = true;
                throwProgress = 0f;
            }
        }

        if (isReturning)
        {
            throwProgress += throwSpeed * Time.deltaTime / Vector3.Distance(throwPosition, boomerangLocation.position);
            throwProgress = Mathf.Clamp01(throwProgress);

            Vector3 straightPos = Vector3.Lerp(throwPosition, boomerangLocation.position, throwProgress);

            // Negative sineOffset flips the arc to the opposite side on return
            float sineOffset = Mathf.Sin(throwProgress * Mathf.PI) * arcHeight;
            boomerang.transform.position = straightPos + -throwRight * sineOffset;

            if (boomerang.transform.position == boomerangLocation.position)
            {
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

        if (Physics.Raycast(boomerangLocation.transform.position, playerCamera.transform.forward, out hit, boomerangDistance, layerMask))
        {
            throwPosition = hit.point;
            boomerang.transform.parent = null;
            rotator.enabled = true;
            isThrown = true;
            throwProgress = 0f;
            throwStartPosition = boomerang.transform.position;
            throwRight = Vector3.Cross((throwPosition - throwStartPosition).normalized, Vector3.up);
        }
        else
        {
            throwPosition = boomerangLocation.position + playerCamera.transform.forward * boomerangDistance;
            boomerang.transform.parent = null;
            returnPosition = boomerangLocation.position;
            rotator.enabled = true;
            isThrown = true;
            throwProgress = 0f;
            throwStartPosition = boomerang.transform.position;
            throwRight = Vector3.Cross((throwPosition - throwStartPosition).normalized, Vector3.up);
        }
    }

    public bool IsActive()
    {
        return isThrown || isReturning;
    }
}
