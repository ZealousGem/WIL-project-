using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockPuzzle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     int[] num;
   

   [SerializeField]
    TMP_Text[] numUI;

     [SerializeField]
    List<int> correctNum;

     [SerializeField]
    GameObject Interact;

    [SerializeField]
     int changedIndex = 0;

     bool displayed = false;

     bool inBox2 = false;

    [HideInInspector]
   public bool PuzzleCompleted;

     [SerializeField]
     int index = 0;

    [SerializeField]
    DoorMovement gate;

    [SerializeField]
     Vector3 uiOffset = new Vector3(0, 1f, 0);

    Camera mainCam;

    PlayerController player;


    private void OnDisable()
    {
        EventBus.Unsubscribe<InputChangeEvent>(ChangingCameras);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<InputChangeEvent>(ChangingCameras);
    }

    void ChangingCameras(InputChangeEvent data)
    {
        mainCam = data.cam.GetComponent<Camera>();
    }

    void Start()
    {
        PuzzleCompleted = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        num = new int[numUI.Length];
        num[0] = 0;
        numUI[0].text = num[0].ToString();
        num[1] = 0;
        numUI[1].text = num[1].ToString();
        num[2] = 0;
        numUI[2].text = num[3].ToString();
        num[3] = 0;
        numUI[3].text = num[3].ToString();


    }

    void ShowBox()
    {
        player.StopMoving();
        displayed = true;
        player.enabled = false;
        CameraChangeEvent cam2 = new CameraChangeEvent(changedIndex, 0f);
        EventBus.Act(cam2);
     
      
        CheckNum();

    }

     void HideBox()
    {
        displayed = false;
        player.enabled = true;
        CameraChangeEvent cam = new CameraChangeEvent(index, 0f);
        EventBus.Act(cam);

    }

     void OnTriggerEnter(Collider other)
    {
        if (!PuzzleCompleted)
        {
            if (other.CompareTag("Player"))
            {
                Interact.SetActive(true);

                inBox2 = true;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Interact.SetActive(false);
            inBox2 = false;
        }
    }

    public void CheckNum()
    {

        if (correctNum == null)
        {
            Debug.LogWarning("Correct combination not set yet!");
            return;
        }
        if (num[0] == correctNum[0] && num[1] == correctNum[1] && num[2] == correctNum[2] && num[3] == correctNum[3] && num.Count() >= 3)
        {
      
           gate.Open(player.transform.position);
           HideBox();
           PointerEvent pointer = new PointerEvent(PointerArrowTypes.Arrow4, false);
           EventBus.Act(pointer);
           PointerEvent pointer2 = new PointerEvent(PointerArrowTypes.Arrow5, true);
           EventBus.Act(pointer2);
           PuzzleCompleted = true;
           inBox2 = false;
           if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("tutorial");
            }
        }

       
    }

   public void arrowUp1()
    {
       
        if (num[0] < 9 && num.Count() >= 3)
        {
            num[0] += 1;
            numUI[0].text = num[0].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

        CheckNum();
    }

    public void arrowUp2()
    {
     
        if (num[1] < 9 && num.Count() >= 3)
        {
            num[1] += 1;
            numUI[1].text = num[1].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

     public void arrowUp3()
    {
     
        if (num[2] < 9 && num.Count() >= 3)
        {
            num[2] += 1;
            numUI[2].text = num[2].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

     public void arrowUp4()
    {
     
        if (num[3] < 9 && num.Count() >= 3)
        {
            num[3] += 1;
            numUI[3].text = num[3].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

   public void arrowDown1()
    {
       
        if (num[0] > 0 && num.Count() >= 3)
        {
            num[0] -= 1;
            numUI[0].text = num[0].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

  public void arrowDown2()
    {
      
        if (num[1] > 0 && num.Count() >= 3)
        {
            num[1] -= 1;
            numUI[1].text = num[1].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

     public void arrowDown3()
    {
      
        if (num[2] > 0 && num.Count() >= 3)
        {
            num[2] -= 1;
            numUI[2].text = num[2].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

     public void arrowDown4()
    {
      
        if (num[3] > 0 && num.Count() >= 3)
        {
            num[3] -= 1;
            numUI[3].text = num[3].ToString();
        }

        if (SoundManager.Instance != null)
        {
          SoundManager.Instance.PlaySound("pickup");
        }

         CheckNum();
    }

    void UpdateUIPosition()
    {
        if (Interact != null)
        {
            Interact.transform.position = transform.position + uiOffset;

            Vector3 camDirection = Interact.transform.position - mainCam.transform.position;
            Interact.transform.rotation = Quaternion.LookRotation(camDirection);
        }
    }

    void Update()
    {
          if (!PuzzleCompleted)
        {
            if (inBox2)
            {
              UpdateUIPosition();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (displayed)
                    {
                        HideBox();
                    }

                    else
                    {
                        ShowBox();
                    }

                }
            }
        }
    }

   
}
