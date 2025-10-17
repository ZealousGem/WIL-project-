using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using TMPro;

public class TutorialPuzzle : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool inBox2 = false;

    bool displayed = false; 

    [SerializeField]
    GameObject Interact;

    PlayerController player;
    
    [SerializeField]
    List<Image> addedItems;

    int requiredCounter = 4;
    
    [SerializeField]
    List<itemSO> requiredItems;

    [SerializeField]
    List<TMP_Text> text;

    Vector3 it;

    public int changedIndex = 0;

    public int index = 0;

    public Sprite image;

    bool PuzzleCompleted = false;

    Camera cam;

    Vector3 offSet;

    Plane draggedPlane;

    Image tempItemPrefab;
    GameManager Game;

     private void OnDisable()
    {
        
        EventBus.Unsubscribe<PuzzleEvent>(Inventory);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<PuzzleEvent>(Inventory);
    }

    public void Inventory(PuzzleEvent data)
    {        
        if (data.puzzle != this) 
    {
        return; // Ignore the event if it's not meant for this puzzle instance
    }
        additem(data.go, data.go2);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Game = GameObject.FindGameObjectWithTag("signleton").GetComponent<GameManager>();
        Interact.SetActive(false);
        for (int i = 0; i < addedItems.Count; i++)
        {
            addedItems[i].color = Color.clear;
        }
        
        for (int i = 0; i < text.Count; i++)
        {
            text[i].text = requiredItems[i].name;
        }

        addedItems[1].color = Color.white;
        addedItems[1].sprite = image;
       
    }

    // Update is called once per frame
    void additem(Image image, Image FoundImage)
    {
        
        for (int i = 0; i < addedItems.Count; i++)
        {

            if (FoundImage == addedItems[i] && addedItems[i].color == Color.clear)
            {

                for (int j = 0;  j <requiredItems.Count; j++)
                {
                    if (image.sprite == requiredItems[j].obj)
                    {
                       addedItems[i].sprite = image.sprite;
                       addedItems[i].color = Color.white;
                       CheckedEvent check = new CheckedEvent(true);
                       EventBus.Act(check);
                    }
                }
                break;
            }

        }

        CheckIfCorrect();
    }

    void CheckIfCorrect()
    {
        int counter = 0;
        for (int i = 0; i < requiredItems.Count; i++)
        {
            if (addedItems[i].sprite == requiredItems[i].obj)
            {
                counter++;
            }
        }

        if (counter == requiredCounter)
        {
            // solved puzzle
         //   Debug.Log("solved puzzle");
            HideBox();
            PuzzleCompleted = true;
            Interact.SetActive(false);
            Game.PuzzleCompleted(1);
            // ChangeDialogueState change = new ChangeDialogueState(DialougeChange.TutorialPuzzle, 1);
            // EventBus.Act(change);
        }

        else
        {
          //  Debug.Log("not solved");

        }
    }

    void Update()
    {
        if (!PuzzleCompleted)
        {
            if (inBox2)
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

        
    }

    void ShowBox()
    {
        displayed = true;
        player.enabled = false;
        CameraChangeEvent cam2 = new CameraChangeEvent(changedIndex, 0f);
        EventBus.Act(cam2);
        InventoryUI.inPuzzle = true;
        cam = Camera.main;
        draggedPlane = new Plane(cam.transform.forward, transform.position);
        CheckIfCorrect();
        
 
    }



    void HideBox()
    {
        displayed = false;
        player.enabled = true;
        CameraChangeEvent cam = new CameraChangeEvent(index, 0f);
        EventBus.Act(cam);
        InventoryUI.inPuzzle = false;
        
       
    }
    
     void OnTriggerEnter(Collider other)
    {
        if (!PuzzleCompleted)
        {
            if (other.CompareTag("Player"))
            {
            Interact.SetActive(true);

            inBox2 = true;
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            Interact.SetActive(false);
            inBox2 = false;
        }
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject ob = eventData.pointerCurrentRaycast.gameObject;
            for (int i = 0; i < addedItems.Count; i++)
            {
                if (addedItems[i].color == Color.white && ob == addedItems[i].gameObject)
                {
                    it = addedItems[i].transform.position;
                 //   Debug.Log("clicked");
                    tempItemPrefab = ob.GetComponent<Image>();
                    tempItemPrefab.raycastTarget = false;
                    
                    // Calculate the offset so the item doesn't jump to the mouse's center
                    Ray ray = cam.ScreenPointToRay(eventData.position);
                    float distance;
                    if (draggedPlane.Raycast(ray, out distance))
                    {
                        offSet = it - ray.GetPoint(distance);
                    }
                       tempItemPrefab.transform.SetAsLastSibling();
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
            Ray ray = cam.ScreenPointToRay(eventData.position);
            float distance;
            
            if (draggedPlane.Raycast(ray, out distance))
            {
                Vector3 newPosition = ray.GetPoint(distance) + offSet;
                tempItemPrefab.transform.position = newPosition;
            }
        }
    }

   public void OnPointerUp(PointerEventData eventData)
{
   
    if (tempItemPrefab == null)
    {
        return;
    }

    
    GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;
    // -----------------------------------------------------------------

   
    if (dropTarget != null)
    {
        
        if (dropTarget.CompareTag("Inventory") && dropTarget.GetComponent<Image>())
        {
            Image hitImage = dropTarget.GetComponent<Image>();
            Sprite tempSprite = tempItemPrefab.sprite;
            Color tempColor = tempItemPrefab.color;

            tempItemPrefab.sprite = hitImage.sprite;
            tempItemPrefab.color = hitImage.color;

            hitImage.sprite = tempSprite;
            hitImage.color = tempColor;
            CheckIfCorrect();

           // Debug.Log("Item dropped on world-space inventory!");
        }
    }

    // Always return the dragged item to its original position
    // and reset the raycast target property and prefab variable
    tempItemPrefab.transform.position = it;
    tempItemPrefab.raycastTarget = true;
    tempItemPrefab = null;
}
}
