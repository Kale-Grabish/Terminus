using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY; 
    [SerializeField] float speedZ;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speedX, speedY, speedZ);
    }
}
