using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Collections;
using UnityEngine;


public enum DialougeChange
{

    TutorialPuzzle,
  
    none,

    TaxiRoute, 

    BikeRoute,

    FoundCarJack,

}

public class CharacterInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    BaseCharacterState curCharState;
    ChoiceDialogueState dialogue = new ChoiceDialogueState();

    

    bool inBox;

    [SerializeField]
    GameObject Interact;

    List<DialogueTree> dialogueNodes = new List<DialogueTree>(); // Editable in Inspector

    [SerializeField]
    public List<NPCDialogue> dialogueState;

    [SerializeField]
    DialogueSystem pepe;
    string NPCname;

    public PointerArrowTypes arrow;

    public int id;

    int curId;

    int RepeatDialogueIndex;

    DialogueTree fuck;

    int curIndex;

    int RootNode = 0;

    public Vector3 uiOffset = new Vector3(0, 1f, 0);

    public List<DialougeChange> eventChange;

    private Camera mainCam;

    void Awake()
    {
        dialogue = new ChoiceDialogueState();

    }

    void Start()
    {
        Interact.SetActive(false);
        inBox = false;
        dialogueNodes = dialogueState[0].dialogueNodes;
        NPCname = dialogueState[0].name;
        RepeatDialogueIndex = dialogueState[0].RepeatDialogueIndex;
        curCharState = dialogue;
        curIndex = dialogueNodes[RootNode].id;
        fuck = dialogueNodes[RootNode];
        curId = dialogueState[0].CharacterId;
        
        // DialogueCheckEvent tree = new DialogueCheckEvent(dialogueNodes[RootNode]);
        // EventBus.Act(tree);



    }

    void OnEnable()
    {
        EventBus.Subscribe<ChangeStateEvent>(SwitchState);
        EventBus.Subscribe<DialogueEndedEvent>(ChangeCurIndex);
        EventBus.Subscribe<ChangeDialogueState>(ChangeDialogueState);
        EventBus.Subscribe<InputChangeEvent>(ChangingCameras);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<ChangeStateEvent>(SwitchState);
        EventBus.Unsubscribe<DialogueEndedEvent>(ChangeCurIndex);
        EventBus.Unsubscribe<ChangeDialogueState>(ChangeDialogueState);
        EventBus.Unsubscribe<InputChangeEvent>(ChangingCameras);
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

     void ChangingCameras(InputChangeEvent data)
    {
        mainCam = data.cam.GetComponent<Camera>();
    }

    void ChangeDialogueState(ChangeDialogueState data)
    {
        foreach (DialougeChange option in eventChange)
        {
            if (data.change == option)
            {
                Change(data.index);
              
//                Debug.Log(option+ NPCname);
                break;
            }
        }
    }

    void Change(int index)
    {
        for (int i = 0; i < dialogueState.Count; i++)
        {
            if (i == index)
            {
                dialogueNodes = dialogueState[index].dialogueNodes;
                NPCname = dialogueState[index].Name;
                RepeatDialogueIndex = dialogueState[index].RepeatDialogueIndex;
                curIndex = dialogueNodes[RootNode].id;
                fuck = dialogueNodes[RootNode];
                curId = dialogueState[index].CharacterId;
                curCharState.ChangeState(NPCname);
              //  Debug.Log("changed");
                break;
            }
        }
    }

    void ChangeCurIndex(DialogueEndedEvent data)
    {
      
        if (data.curState == DialogueState.NextDialogue && data.name == NPCname)
        {
              Interact.SetActive(true);
          //  Debug.Log("triggered");
            if (curIndex == data.id)
            {
                
                curCharState.ChangeState(NPCname);
                curIndex = RepeatDialogueIndex;
                inBox = true;
            }

            else
            {
                curIndex = data.id;
            //    Debug.Log(data.id + gameObject.name + data.name);
                inBox = true;
            }
            foreach (DialogueTree x in dialogueNodes)
            {
                if (curIndex == x.id)
                {
                    // DialogueCheckEvent tree = new DialogueCheckEvent(x);
                    // EventBus.Act(tree);
                  //  DialogueSystem pepe = GameObject.FindWithTag("Finish").GetComponent<DialogueSystem>();
                    pepe.ChangeTree(x);
                    fuck = x;
                    break;
                }

            }

        }
        
        else if (data.curState == DialogueState.Ended && data.name == NPCname)
        {
           // Debug.Log("finished");
             Interact.SetActive(true);
            if (curIndex != data.id)
            {

                curIndex = data.id;


            }
           
            foreach (DialogueTree x in dialogueNodes)
            {
                if (curIndex == x.id)
                {
                    // DialogueCheckEvent tree = new DialogueCheckEvent(x);
                    // EventBus.Act(tree);
                  //  DialogueSystem pepe = GameObject.FindWithTag("Finish").GetComponent<DialogueSystem>();
                    pepe.ChangeTree(x);
                    fuck = x;
                    curCharState.EnterState(NPCname, curIndex);
                    break;
                }

                else
                {
                 //   Debug.Log("can't find it");
                }

            }
        }      


    }

    // Update is called once per frame
    void Update()
    {
        if (inBox && id == curId)
        {
            UpdateUIPosition();
            if (Input.GetKeyDown(KeyCode.E))
            {




                //DialogueSystem pepe = GameObject.FindWithTag("Finish").GetComponent<DialogueSystem>();
                pepe.ChangeTree(fuck);
                curCharState.EnterState(NPCname, curIndex);
                //    Debug.Log(curIndex);
                Interact.SetActive(false);
                inBox = false;
                CheckArrow();
                // Debug.Log("clicked " + gameObject.name + "id " + curId);



            }
        }
    }
    
    void CheckArrow()
    {
        if (arrow != PointerArrowTypes.none)
        {
            PointerEvent pointer = new PointerEvent(arrow, false);
            EventBus.Act(pointer);
        }
        
        else
        {
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact.SetActive(true);
            //id = gameObject.name;
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

    void SwitchState(ChangeStateEvent newState)
    {

        if (NPCname == newState.name)
        {
           curCharState = newState.state;
        }
      

       // Debug.Log(NPCname);
    }
}
