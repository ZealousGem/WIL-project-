using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Collections;
using UnityEngine;




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

    
    string NPCname;

    int RepeatDialogueIndex;


    int curIndex;

    int RootNode = 0;

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
        DialogueCheckEvent tree = new DialogueCheckEvent(dialogueNodes[RootNode]);
        EventBus.Act(tree);
        

    }

    void OnEnable()
    {
        EventBus.Subscribe<ChangeStateEvent>(SwitchState);
        EventBus.Subscribe<DialogueEndedEvent>(ChangeCurIndex);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<ChangeStateEvent>(SwitchState);
        EventBus.Unsubscribe<DialogueEndedEvent>(ChangeCurIndex);
    }

    void ChangeCurIndex(DialogueEndedEvent data)
    {
        if (data.curState == DialogueState.NextDialogue && data.name == NPCname)
        {

            if (curIndex == data.id)
            {

                curCharState.ChangeState();
                curIndex = RepeatDialogueIndex;
                inBox = true;
            }

            else
            {
                curIndex = data.id;
                inBox = true;
            }
            foreach (DialogueTree x in dialogueNodes)
            {
                if (curIndex == x.id)
                {
                    DialogueCheckEvent tree = new DialogueCheckEvent(x);
                    EventBus.Act(tree);
                }

            }

        }
        
        else if (data.curState == DialogueState.Ended)
        {
          
            if (curIndex != data.id)
            {

                curIndex = data.id;


            }
           
            foreach (DialogueTree x in dialogueNodes)
            {
                if (curIndex == x.id)
                {
                    DialogueCheckEvent tree = new DialogueCheckEvent(x);
                    EventBus.Act(tree);
                    curCharState.EnterState(NPCname, curIndex);
                    
                    break;
                }

                else
                {
                    Debug.Log("can't find it");
                }

            }
        }      


    }

    // Update is called once per frame
    void Update()
    {
        if (inBox)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {


                curCharState.EnterState(NPCname, curIndex);

                inBox = false;




            }
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

    void SwitchState(ChangeStateEvent newState)
    {
        curCharState = newState.state;
    }
}
