using UnityEditor.Search;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool inBox = false;

    bool displayed = false; 

    [SerializeField]
    GameObject Interact;

    [SerializeField]
    GameObject UICam;

    GameObject currentCam;

    //Camera inGameCam;

    PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Interact.SetActive(false);
    }

    // Update is called once per frame
    
    void Update()
    {
        if (inBox)
        {

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

    void ShowBox()
    {
        displayed = true;
        currentCam = GameObject.FindGameObjectWithTag("MainCamera");
        player.enabled = false;
        if (currentCam.activeSelf)
        {
            
            currentCam.SetActive(false);
            UICam.SetActive(true);

        }

    }

   

    void HideBox()
    {
        displayed = false;
        player.enabled = true;
        if (UICam.activeSelf && currentCam != null)
        {
            currentCam.SetActive(true);
            UICam.SetActive(false);
            

        } 
    }
    
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact.SetActive(true);

            inBox = true;




        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact.SetActive(false);
            inBox = false;
        }
    }
}
