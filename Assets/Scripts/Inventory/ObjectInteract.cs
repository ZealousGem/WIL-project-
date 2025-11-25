using Unity.VisualScripting;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public itemSO item;
    public GameObject interact;


    public Vector3 uiOffset = new Vector3(0, 1f, 0);

    bool inBox = false;

    private Camera mainCam;

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

    private void Start()
    {
        interact.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            interact.SetActive(true);
            inBox = true;
           
        }
    }


    void Update()
    {
        if (inBox)
        {
            UpdateUIPosition();
         if (Input.GetKeyDown(KeyCode.E))
        {

             

            GameObjectEvent obj = new GameObjectEvent(this.gameObject);
            EventBus.Act(obj);
            if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("obj");
            }
            itemEvent it = new itemEvent(item);
            EventBus.Act(it);



        }
        }
        
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.SetActive(false);
            inBox = false;
        }

    }

    void UpdateUIPosition()
    {
        if (interact != null)
        {
            interact.transform.position = transform.position + uiOffset;

            Vector3 camDirection = interact.transform.position - mainCam.transform.position;
            interact.transform.rotation = Quaternion.LookRotation(camDirection);
        }
    }
}
