using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public itemSO item;
    GameObject obj;


    public InventoryItem(itemSO curObj)
    {
        item = curObj;

    }

    //   [SerializeField] Image iconImage;

}

public class InventoryManager : MonoBehaviour
{



    int size = 0;
    public List<InventoryItem> items;
    GameObject obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {     
         
        items = new List<InventoryItem>();
    }

    private void OnDisable()
    {
        // observer.RemoveObserver(this);
        EventBus.Unsubscribe<GameObjectEvent>(GameObjectTrans);
        EventBus.Unsubscribe<itemEvent>(itemTrans);
        EventBus.Unsubscribe<RemoveEvent>(RemoveInventory);
    }

    private void OnEnable()
    {
        //  observer.AddObersver(this);
        EventBus.Subscribe<GameObjectEvent>(GameObjectTrans);
        EventBus.Subscribe<itemEvent>(itemTrans);
        EventBus.Subscribe<imageEvent>(Inventory);
          EventBus.Subscribe<RemoveEvent>(RemoveInventory);
    }

    public void RemoveInventory(RemoveEvent Sprite)
    {
        RemoveItem(Sprite.go);
    }

     public void Inventory(imageEvent data)
    {
        RemoveItem(data.go.sprite);
    }


    void GameObjectTrans(GameObjectEvent data)
    {
       // Debug.Log("here");
        findObj(data.go);
      //  DestroyObject(data.go);
    }

    GameObject findObj(GameObject temp)
    {
       // Debug.Log("2");
        obj = temp;
        return obj;
    }

    void itemTrans(itemEvent data)
    {
        PickupedItem(data.go);
    }


    void PickupedItem(itemSO item)
    {
        if (items.Count < 6)
        {
            InventoryItem itemTemp = new InventoryItem(item);
            items.Add(itemTemp);
            // Debug.Log("worked");
            size = items.Count - 1;
            itemUIEvent ui = new itemUIEvent(item, size);
            EventBus.Act(ui);
          //  observer.TellObervers(PlayerActions.ActionSeven, itemTemp.item);
            try { Destroyobj(); } catch { }

        }

        else
        {
            Debug.Log("you are at the max of what you can carry");
        }

       // observer.TellObervers(PlayerActions.ActionOne, items);
    }

    public void RemoveItem(Sprite image)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (image == items[i].item.obj)
            {
                items.Remove(items[i]);
                break;
            }
        }
    }

   

    GameObject Destroyobj()
    {
        Destroy(obj);
        obj = null;
        return obj;
    }

    void RemoveItem()
    {
        if (items.Count == 0 || size >= items.Count) return;
        SpawnItem(items[size]);

      //  observer.TellObervers(PlayerActions.Actionagt, items[size].item);
        Debug.Log("lost " + items[size].item);
        items.RemoveAt(size);
        if (size >= items.Count)
        {
            size = Mathf.Max(0, items.Count - 1);


        }

        if (items.Count > 0)
        {
          //  observer.TellObervers(PlayerActions.ActionSeven, items[size].item);

        }

       

       // observer.TellObervers(PlayerActions.ActionOne, items);
    }

    void SpawnItem(InventoryItem item)
    {

        GameObject DroppedObject = item.item.prefab;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 pos = (Vector2)player.transform.position + new Vector2(0, 1);

        Vector3 SpawnPoint = new Vector3(pos.x, pos.y, 0);
        Instantiate(DroppedObject, SpawnPoint, Quaternion.identity);

    }

   

  
}