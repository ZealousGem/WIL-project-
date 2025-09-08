using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool inBox = false;

    bool displayed = false; 

    [SerializeField]
    GameObject Interact;

    [SerializeField]
    GameObject UICam;

    GameObject currentCam;

    PlayerController player;
    
    [SerializeField]
    List<Image> addedItems;

    int requiredCounter = 12;
    
    [SerializeField]
    List<itemSO> requiredItems;

    Vector3 it;

    Camera cam;

    Image tempItemPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Interact.SetActive(false);
        for (int i = 0; i < addedItems.Count; i++)
        {
            addedItems[i].color = Color.clear;
        }
    }

    // Update is called once per frame
    void additem(Image image, Image FoundImage)
    {
        for (int i = 0; i < addedItems.Count; i++)
        {

            if (FoundImage == addedItems[i])
            {
                addedItems[i].sprite = image.sprite;
            }
            
        }
    }

    void CheckIfCorrect()
    {
        int counter = 0;
        for (int i = 0; i < requiredItems.Count; i++)
        {
            if (addedItems[i].sprite == requiredItems[i])
            {
                counter++;
            }
        }

        if (counter == requiredCounter)
        {
            // solved puzzle
        }
    }

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
            cam = UICam.GetComponent<Camera>();
            cam = Camera.main;

        }
 
    }

   

    void HideBox()
    {
        displayed = false;
        player.enabled = true;
        if (UICam.activeSelf && currentCam != null)
        {
            currentCam.SetActive(true);
            cam = currentCam.GetComponent<Camera>();
            cam = Camera.main;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
       
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject ob = eventData.pointerCurrentRaycast.gameObject;

          
            for (int i = 0; i < addedItems.Count; i++)
            {
                if (addedItems[i].color == Color.white && ob == addedItems[i].gameObject)
                {
                    //tempIamge.Add(itemUI[i].gameObject);
                    it =addedItems[i].transform.position;
                  //  Debug.Log("clicked");
                    tempItemPrefab = ob.GetComponent<Image>();
                    tempItemPrefab.raycastTarget = false;
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
        //  throw new System.NotImplementedException();
        
          GameObject ob = eventData.pointerCurrentRaycast.gameObject;


          if (tempItemPrefab != null)
        {
            if (ob != null)
            {
                if (ob.gameObject.CompareTag("inventroy"))
                {


                    if (ob.GetComponent<Image>())
                    {

                        Image hitImage = ob.GetComponent<Image>();
                        Image switchImage = tempItemPrefab;
                        for (int i = 0; i < addedItems.Count; i++)
                        {
                            if (hitImage == addedItems[i])
                            {
                                addedItems[i].sprite = switchImage.sprite;
                            }

                            if (tempItemPrefab == addedItems[i])
                            {
                                addedItems[i].sprite = hitImage.sprite;
                            }
                        }

                    }
                    tempItemPrefab.transform.position = it;
                  
                    //  Debug.Log("working");

                }

                else
                {
                    //  Debug.Log("not working");
                    tempItemPrefab.transform.position = it;
                    // tempItemPrefab = null;
                }
            }

            else
            {
                //   Debug.Log("not working");
                tempItemPrefab.transform.position = it;
                // tempItemPrefab = null;
            }

            tempItemPrefab.raycastTarget = true;
            tempItemPrefab = null;
        }


       
        
    }
}
