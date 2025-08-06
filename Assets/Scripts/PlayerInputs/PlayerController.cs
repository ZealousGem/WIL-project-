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
    bool shouldRotate = false;
    Quaternion rotate;

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
        //Rotate();

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
                    agent.destination = hit.point;
                    

                  //  gameObject.transform.LookAt(movepls);
                }
            }
        }
    }

   
}
