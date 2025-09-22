using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    // This is a reference to the child door mesh, assigned in the Inspector.
    public GameObject door;

    bool isOpen = false;

     public string id;

    // Use a fixed rotation amount.
    float rotationAmount = 90f;
    float speed = 1f;
    
    Vector3 forwardDirection;
    
    Coroutine animationCoroutine;
    Quaternion startRotation;


    void Awake()
    {
        // The script is on the pivot, so its transform is our pivot.
        // We store the initial rotation of the pivot.
        startRotation = transform.localRotation;

        // The forward direction is relative to the pivot.
        forwardDirection = transform.forward;
    }

   



   public void Open(Vector3 userPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            
            animationCoroutine = StartCoroutine(RotateDoor(userPosition, true));
        }
    }

    void Closed()
    {
        if (isOpen)
        {
           //  try { SoundManager.Instance.PlaySound("door"); } catch { }
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            
            animationCoroutine = StartCoroutine(RotateDoor(Vector3.zero, false));
        }
    }
    
    IEnumerator RotateDoor(Vector3 userPosition, bool open)
    {
        Quaternion startRot = transform.localRotation;
        Quaternion endRot;

        if (open)
        {
            // The dot product determines which side of the door the player is on.
            float dot = Vector3.Dot(forwardDirection, (userPosition - transform.position).normalized);

            if (dot >= 0)
            {
                // Open forward (relative to the pivot's local rotation).
                endRot = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, -rotationAmount, 0));
            }
            else
            {
                // Open backward.
                endRot = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, rotationAmount, 0));
            }

            isOpen = true;
        }
        else
        {
            endRot = startRotation;
            isOpen = false;
        }
        
        float time = 0;
        while (time < 1)
        {
            // We rotate the parent pivot object.
            transform.localRotation = Quaternion.Slerp(startRot, endRot, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

        // Ensure the rotation is perfectly set at the end.
        transform.localRotation = endRot;
    }

    
}