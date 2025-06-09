using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    NavMeshAgent agent;
    Camera cam;


    public float rotationSpeed;
    bool shouldRotate = false;
    Quaternion rotate;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotate();
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
                    agent.SetDestination(hit.point);
                    rotate = Quaternion.LookRotation(hit.point);
                    shouldRotate = true;

                  //  gameObject.transform.LookAt(movepls);
                }
            }
        }
    }

    void Rotate()
    {
        if (shouldRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate,rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotate) < 1f)
            {
                shouldRotate = false;
            }
        }
    }
}
