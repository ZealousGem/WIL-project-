using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    List<InventoryItem> items = new List<InventoryItem>();

    public List<Image> InvUI;

    int size = 0;

    public GameObject UI;

    bool displayed = false;

    void OnEnable()
    {
         EventBus.Subscribe<itemUIEvent>(itemTrans);
    }

    void OnDisable()
    {
         EventBus.Unsubscribe<itemUIEvent>(itemTrans);
    }

    void itemTrans(itemUIEvent data)
    {
        InventoryItem item = new InventoryItem(data.go);
        AddItem(item);
        size = data.index;
        showItem();
    }
    
    void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    void Start()
    {
        UI.SetActive(false);
    }

    public void ShowInvUi()
    {
        if (displayed)
        {
            UI.SetActive(false);
            displayed = false;
        }

        else
        {
            showItem();
            UI.SetActive(true);
            displayed = true;
        }
    }

    void showItem()
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                InvUI[i].color = Color.white;
                InvUI[i].sprite = items[i].item.obj;
            }

            for (int i = items.Count; i < InvUI.Count; i ++)
             {
                 InvUI[i].color = Color.clear;
                
             }
            
        }

        else
        {
            for (int i = 0; i < InvUI.Count; i++)
            {
               InvUI[i].color = Color.clear;
            }
        }
        
    }

    // Update is called once per frame
    void RemoveItem()
    {
        // will do this later
    }
}
