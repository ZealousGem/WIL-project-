using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  bool inBox3 = false;

  public string SceneName;

[SerializeField]
    GameObject Interact;

      [SerializeField]
     Vector3 uiOffset = new Vector3(0, 1f, 0);

    Camera mainCam;

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
         mainCam = data.cam.GetComponent<Camera>();

    }

     void OnTriggerEnter(Collider other)
    {
        
            if (other.CompareTag("Player"))
            {
                Interact.SetActive(true);

                inBox3 = true;
            }
        

    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Interact.SetActive(false);
            inBox3 = false;
        }
    }

    // Update is called once per frame
   void Update()
    {
          
            if (inBox3)
            {
              UpdateUIPosition();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(SceneName);
                }
            }
        
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
}
