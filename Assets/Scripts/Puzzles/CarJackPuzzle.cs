using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class CarJackPuzzle : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool inBox2 = false;

    bool displayed = false;

    [SerializeField]
    GameObject Interact;

    PlayerController player;

    [SerializeField]
    List<Image> addedItems;

    [SerializeField]
    List<itemSO> requiredItems;

 [SerializeField]
    TMP_Text text;

[SerializeField]
    itemSO CreateItem; 

    Vector3 it;

    bool itemCreated = false;

    public int changedIndex = 0;

    public int index = 0;

    //public PointerArrowTypes arrow;

[HideInInspector]
   public bool PuzzleCompleted;

    Camera cam;

    Vector3 offSet;

    Plane draggedPlane;

    Image tempItemPrefab;

    public GameObject TakeButton;
   
   // GameManager Game;

    public Vector3 uiOffset = new Vector3(0, 1f, 0);

    private Camera mainCam;


    private void OnDisable()
    {

        EventBus.Unsubscribe<CarJackEvent>(Inventory);
        EventBus.Unsubscribe<InputChangeEvent>(ChangingCameras);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<CarJackEvent>(Inventory);
        EventBus.Subscribe<InputChangeEvent>(ChangingCameras);
    }

    void ChangingCameras(InputChangeEvent data)
    {
        mainCam = data.cam.GetComponent<Camera>();
    }

    public void Inventory(CarJackEvent data)
    {
        if (data.puzzle != this)
        {
            return; // Ignore the event if it's not meant for this puzzle instance
        }
        additem(data.go, data.go2);
    }

    void ButtonUI(bool item)
    {
        TakeButton.SetActive(item);
    }

     void additem(Image image, Image FoundImage)
    {

        for (int i = 0; i < addedItems.Count; i++)
        {

            if (FoundImage == addedItems[i] && addedItems[i].color == Color.clear)
            {

                for (int j = 0; j < requiredItems.Count; j++)
                {
                    if (image.sprite == requiredItems[j].obj)
                    {
                        addedItems[i].sprite = image.sprite;
                        addedItems[i].color = Color.white;
                        CheckedEvent check = new CheckedEvent(true);
                        EventBus.Act(check);
                        if (SoundManager.Instance != null)
                        {
                          SoundManager.Instance.PlaySound("drop");
                        }
                    }
                }
                break;
            }

        }

         CheckIfCorrect();
    }

    public void TakeItem()
    {
        HideBox();
        PuzzleCompleted = true;
        for (int i = 0; i < addedItems.Count; i++)
        {
            addedItems[i].color = Color.clear;
        }
        PointerEvent pointer = new PointerEvent(PointerArrowTypes.Arrow6, false);
        EventBus.Act(pointer);
        PointerEvent pointer2 = new PointerEvent(PointerArrowTypes.Arrow2, true);
        EventBus.Act(pointer2);
        ChangeDialogueState change = new ChangeDialogueState(DialougeChange.FoundCarJack, 2);
        EventBus.Act(change);
        itemEvent it = new itemEvent(CreateItem);
        EventBus.Act(it);
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

    void CheckIfCorrect()
    {
        if (addedItems[0].sprite == requiredItems[0].obj && addedItems[1].sprite == requiredItems[1].obj ||
        addedItems[1].sprite == requiredItems[0].obj && addedItems[0].sprite == requiredItems[1].obj)
        {
            addedItems[2].color = Color.white;
            addedItems[2].sprite = CreateItem.obj;
            text.text = CreateItem.name;
            itemCreated = true;
            ButtonUI(itemCreated);
        }
       
    }

     void HideBox()
    {
        displayed = false;
        player.enabled = true;
        CameraChangeEvent cam = new CameraChangeEvent(index, 0f);
        EventBus.Act(cam);
        InventoryUI.inPuzzle = false;
        ButtonUI(false);


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
                if (addedItems[i].color == Color.white && ob == addedItems[i].gameObject && ob != addedItems[2].gameObject)
                {
                    it = addedItems[i].transform.position;
                    //   Debug.Log("clicked");
                    tempItemPrefab = ob.GetComponent<Image>();
                    tempItemPrefab.raycastTarget = false;

                    cam = Camera.main; // Or use mainCam if you know it's the right one
                    draggedPlane = new Plane(cam.transform.forward, it);

                    // Calculate the offset so the item doesn't jump to the mouse's center
                    Ray ray = cam.ScreenPointToRay(eventData.position);
                    float distance;
                    if (draggedPlane.Raycast(ray, out distance))
                    {
                        Vector3 Clicked = ray.GetPoint(distance);
                        offSet = it - Clicked;
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
                 if (SoundManager.Instance != null)
                    {
                      SoundManager.Instance.PlaySound("drop");
                    }

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


    void UpdateUIPosition()
    {
        if (Interact != null)
        {
            Interact.transform.position = transform.position + uiOffset;

            Vector3 camDirection = Interact.transform.position - mainCam.transform.position;
            Interact.transform.rotation = Quaternion.LookRotation(camDirection);
        }
    }

    void Start()
    {
         PuzzleCompleted = true;
        TakeButton.SetActive(false);
         player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      //  Game = GameObject.FindGameObjectWithTag("Pointer").GetComponent<GameManager>();
        Interact.SetActive(false);
        for (int i = 0; i < addedItems.Count; i++)
        {
            addedItems[i].color = Color.clear;
        }

            text.text = "";
        

        
    }

    // Update is called once per frame
    void Update()
    {
          if (!PuzzleCompleted)
        {
            if (inBox2)
            {
              UpdateUIPosition();
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
}