using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Drawing;
using UnityEditor.Analytics;


public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    NavMeshAgent agent;
    Camera cam;

    Animator animator;

    public float rotationSpeed;
    bool ReachedDestination = true;

    public float movementThreshold = 0.1f;


    float currentSpeed = 0;

    void OnEnable()
    {
        EventBus.Subscribe<InputChangeEvent>(ChangingCameras);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<InputChangeEvent>(ChangingCameras);
    }

    void ChangingCameras(InputChangeEvent data)
    {
        cam = data.cam.GetComponent<Camera>();
    }



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AnimateCharacter();
        Rotate();


    }

    void AnimateCharacter()
    {
        if (agent.velocity.magnitude > 0.1f)
        {

            // Debug.Log(ReachedDestination);
            ReachedDestination = false;
        }

        else
        {
            ReachedDestination = true;
            //   Debug.Log(ReachedDestination);
        }
        
         currentSpeed = agent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);

    }

    void Movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray raycast = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(raycast, out RaycastHit hit))
            {
                if (hit.collider.gameObject)
                {
                    NavMeshHit hitted;

                    if (NavMesh.SamplePosition(hit.point, out hitted, 1.0f, NavMesh.AllAreas)) {
                         agent.destination = hitted.position;
                    }
                   
                   

                    //  gameObject.transform.LookAt(movepls);
                }
            }
        }
    }
    
    void Rotate()
    {
        // Only rotate if the agent has a path and is not at the destination
        if (agent.velocity.magnitude > movementThreshold)
        {
            // Get the direction to the next steering point
            Vector3 direction = agent.steeringTarget - transform.position;
            direction.y = 0; // Keep rotation on the horizontal plane

            if (direction != Vector3.zero)
            {
                // Calculate the rotation needed to look at the steering target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // Smoothly rotate the character towards that direction
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

   
}
