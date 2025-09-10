using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    int index, changedIndex;

    bool NewRoom;

    void Start()
    {
        NewRoom = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {

//            Debug.Log("passed");
            if (!NewRoom)
            {
                  NewRoom = true;
                CameraChangeEvent cam = new CameraChangeEvent(changedIndex, 0f);
                EventBus.Act(cam);
              
            }

            else
            {
                NewRoom = false;
                CameraChangeEvent cam = new CameraChangeEvent(index, 0f);
                EventBus.Act(cam);
                
            }
        }

        
    }


}
