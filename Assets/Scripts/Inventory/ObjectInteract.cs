using Unity.VisualScripting;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public itemSO item;
    public GameObject interact;

    private void Start()
    {
        interact.SetActive(false);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            if (Input.GetKeyDown(KeyCode.E))
            {


                Debug.Log("here");
                GameObjectEvent obj = new GameObjectEvent(this.gameObject);
                EventBus.Act(obj);

                itemEvent it = new itemEvent(item);
                EventBus.Act(it); 



            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.SetActive(true);
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.SetActive(false);
        }
           
    }
}
