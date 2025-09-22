using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InventoryManager inventory;

    public itemSO item;

    bool nearDoor = false;

    bool doorOpened = false; 

    public GameObject interact;
    
    

    void CheckItem()
    {
      
        if (inventory.items != null)
        {

            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].item == item)
                {
                    doorOpened = true;
                    break; 

                }
            }

            if (doorOpened)
            {
                Debug.Log("found matching item");
            }

            else
            {
                Debug.Log("don't have matching item");
            }

        }

        else
        {
              Debug.Log("no items");
        }
        
    }

    void Start()
    {
        interact.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("signleton").GetComponent<InventoryManager>();

        if (inventory == null) {
            Debug.Log("null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!doorOpened)
        {
         if (nearDoor)
         {
            if (Input.GetKeyDown(KeyCode.E))
            {

                CheckItem();

            }
         }
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.SetActive(true);
            nearDoor = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.SetActive(false);
            nearDoor = false;
        }
    }
}
