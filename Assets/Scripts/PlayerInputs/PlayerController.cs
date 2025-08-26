using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    NavMeshAgent agent;
    Camera cam;

    Animator animator;

    public float rotationSpeed;
    bool ReachedDestination = true;
    

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
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AnimateCharacter();
        //Rotate();


    }

    void AnimateCharacter()
    {
        if (!ReachedDestination)
        {
            currentSpeed = agent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
           // Debug.Log(ReachedDestination);
        }

        else
        {
            currentSpeed = 0;
            animator.SetFloat("Speed", currentSpeed);
         //   Debug.Log(ReachedDestination);
        }
        
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
                    ReachedDestination = false;
                    agent.destination = hit.point;
                    if (agent.destination == hit.point)
                    {
                        ReachedDestination = true;
                    }

                    //  gameObject.transform.LookAt(movepls);
                }
            }
        }
    }

   
}
