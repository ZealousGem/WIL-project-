using System.Collections.Generic;
 using UnityEngine.UI;

using UnityEngine;

using Unity.VisualScripting;

using UnityEngine.EventSystems;

using TMPro;

using System;

using System.Collections;



public class InventoryUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler

{

    // Start is called once before the first execution of Update after the MonoBehaviour is created



   public List<InventoryItem> items = new List<InventoryItem>();


    public List<Image> InvUI;


    int size = 0;


    public TMP_Text text;


    public TMP_Text info;


    int curIndex = 0;


    public GameObject UI;


    Image tempItemPrefab;


    bool isPuzzle = false;


    Vector3 it;


    bool displayed = false;


   [HideInInspector]

    public static bool inPuzzle = false;


    void OnEnable()

    {

        EventBus.Subscribe<itemUIEvent>(itemTrans);

        EventBus.Subscribe<spriteEvent>(SpriteChange);

        EventBus.Subscribe<CheckedEvent>(PuzzleEvent);

    }


    void OnDisable()

    {

        EventBus.Unsubscribe<itemUIEvent>(itemTrans);

        EventBus.Unsubscribe<spriteEvent>(SpriteChange);

        EventBus.Subscribe<CheckedEvent>(PuzzleEvent);

    }


    void PuzzleEvent(CheckedEvent data)

    {

        setPuzzle(data.Checked);

    }


    void SpriteChange(spriteEvent data)

    {

        RemoveItem(data.go);

       // Debug.Log("im tired");

    }


    void itemTrans(itemUIEvent data)

    {

        InventoryItem item = new InventoryItem(data.go);

        AddItem(item);

        size = data.index;

        showInv(0);

    }


    void AddItem(InventoryItem item)

    {

        items.Add(item);

        StartCoroutine(TextEvent(item.item, true));

    }


    void setPuzzle(bool setTo)

    {

        isPuzzle = setTo;

    }


    void RemoveItem(Sprite image)

    {

       

        for (int i = 0; i < items.Count; i++)

        {

            if (items[i].item.obj == image)

            {

                StartCoroutine(TextEvent(items[i].item, false));

                items.Remove(items[i]);

                if (curIndex > 0)

                {

                    curIndex--;

                    if (curIndex <= 0)

                    {

                        curIndex = 0;  

                    }    

                }

                break;

            }

        }

        if (displayed)

        {

             showInv(0);

        }

       

    }


    IEnumerator TextEvent(itemSO item, bool state)

    {

        if (state)

        {

            info.text = item.name + " added";



        }


        else

        {

            info.text = item.name + " removed";

        }


        yield return new WaitForSeconds(1f);

        info.text = "";

    }

   

    IEnumerator TextInfo(string parameter)

    {

       

        info.text = parameter;

        yield return new WaitForSeconds(1f);

        info.text = "";

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

            //showItem();

            showInv(curIndex);

            UI.SetActive(true);

            displayed = true;

        }

    }


    public void ClickUpArrow()

    {

        if (0 < curIndex)

        {


            curIndex--;

            showInv(curIndex);


        }


        else

        {

            curIndex = items.Count - 1;

            showInv(curIndex);

        }

    }


    void showInv(int index)

    {

      //  Debug.Log(index);

        if (items.Count > 0)

        {

            InvUI[0].color = Color.white;

            InvUI[0].sprite = items[index].item.obj;

            text.text = items[index].item.name;

        }


        else

        {

            InvUI[0].color = Color.clear;

            text.text = "No Item";

        }


    }


    public void ClickDownArrow()

    {

        if (items.Count - 1 > curIndex)

        {


            curIndex++;

            showInv(curIndex);


        }


        else

        {

            curIndex = 0;

            showInv(curIndex);

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


            for (int i = items.Count; i < InvUI.Count; i++)

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


    public void OnPointerDown(PointerEventData eventData)

    {

        //throw new System.NotImplementedException();

        if (eventData.button == PointerEventData.InputButton.Left && inPuzzle)

        {

            GameObject ob = eventData.pointerCurrentRaycast.gameObject;


            //  List<GameObject> tempIamge = new List<GameObject>();


            for (int i = 0; i < InvUI.Count; i++)

            {

                if (InvUI[i].color == Color.white && ob == InvUI[i].gameObject)

                {

                    // tempIamge.Add(item[i].gameObject);

                    tempItemPrefab = ob.GetComponent<Image>();

                    it = InvUI[i].transform.position;

                    tempItemPrefab.raycastTarget = false;

                    break;

                }

            }

        }

    }


    public void OnDrag(PointerEventData eventData)

    {

        // throw new System.NotImplementedException();

        if (tempItemPrefab != null)

        {

            tempItemPrefab.transform.position = eventData.position;


        }

    }


    public void OnPointerUp(PointerEventData eventData)

    {

        // If we're not dragging an item, do nothing

        if (tempItemPrefab == null)

        {

            return;

        }


        // Check for a UI element in the screen space canvas first

        GameObject ob = eventData.pointerCurrentRaycast.gameObject;


        // If no UI element was hit, try a raycast into the 3D world

        if (ob == null)

        {

            Ray ray = Camera.main.ScreenPointToRay(eventData.position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))

            {

                ob = hit.collider.gameObject;

            }

        }


        // Now, check if the found object (either from UI or world raycast) is a valid inventory slot

        if (ob != null)

        {

            // Use a null check and then CompareTag to be safe

            if (ob.CompareTag("Inventory") && ob.GetComponent<Image>() != null)

            {

                Image hitImage = ob.GetComponent<Image>();


                TutorialPuzzle targetPuzzle = ob.GetComponentInParent<TutorialPuzzle>();

                if (hitImage.color == Color.clear && targetPuzzle != null)

                {

//                    Debug.Log("Found a valid inventory slot in world space!" + ob.gameObject.name);

                    PuzzleEvent puzzle = new PuzzleEvent(tempItemPrefab, hitImage, targetPuzzle);

                    EventBus.Act(puzzle);


                    if (isPuzzle)

                    {

                        imageEvent removeItem = new imageEvent(tempItemPrefab);

                        EventBus.Act(removeItem);

                        RemoveItem(tempItemPrefab.sprite);

                        isPuzzle = false;

                    }

                   

                    else

                    {

                        Debug.Log("Wrong Puzzle");

                        info.text = "wrong shelf";

                        StartCoroutine(TextInfo(info.text));

                    }

                   

                     


                }


                else

                {

                    Debug.Log("Invalid drop target.");

                }



                // You can now proceed with your swap logic here

            }

            else

            {

        //        Debug.Log("Invalid drop target.");

            }

        }

        else

        {

            Debug.Log("No object found to drop on.");

        }


        // Always return the item to its original position regardless of drop success

        tempItemPrefab.transform.position = it;

        tempItemPrefab.raycastTarget = true;

        tempItemPrefab = null;

    }

} 