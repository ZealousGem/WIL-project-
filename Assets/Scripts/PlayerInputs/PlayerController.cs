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

    LayerMask ignoredLayerMask;

    public float rotationSpeed;
    

    public float movementThreshold = 0.1f;

    [HideInInspector]



 [SerializeField] ParticleSystem clickEffect;

    float currentSpeed = 0;

    float agentSpeed = 3.5f;

   
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

    public void StopMoving()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        ignoredLayerMask = ~LayerMask.GetMask("NPC & Doors");
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
        

        currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
       // Debug.Log(currentSpeed);

    }

    void Movement()
    {

        

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {

            agent.isStopped = true;
            agent.speed = 0;
             if (SoundManager.Instance != null)
            {
                
                SoundManager.Instance.StopMusic("move");
            }


        }


        


        if (Input.GetMouseButtonDown(1))
        {
            agent.speed = agentSpeed;
            // cam = Camera.main;
            Ray raycast = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(raycast, out RaycastHit hit, Mathf.Infinity, ignoredLayerMask))
            {
                if (hit.collider.gameObject)
                {
                    NavMeshHit hitted;
                    

                    if (NavMesh.SamplePosition(hit.point, out hitted, 1.0f, NavMesh.AllAreas))
                    {

                        agent.destination = hitted.position;
                        // Debug.Log(hitted.position);
                        agent.isStopped = false;
                        agent.stoppingDistance = 0.1f;
                        if (clickEffect != null)
                        {
                            Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                        }
                        if (SoundManager.Instance != null && Time.timeScale != 0f)
                        {
                          SoundManager.Instance.PlaySound("click");
                          SoundManager.Instance.PlaySound("move");
                        }
                    }



                    //  gameObject.transform.LookAt(movepls);
                }
            }
        }

        //   agent.isStopped = ReachedDestination;

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
              //  Debug.Log(direction);
                // Smoothly rotate the character towards that direction
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


}
