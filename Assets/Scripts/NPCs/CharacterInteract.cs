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

    [SerializeField]
    public List<DialogueTree> dialogueNodes; // Editable in Inspector


    [SerializeField]
    string NPCname;

    [SerializeField]
    int RepeatDialogueIndex;

    [SerializeField]
    int[] DialogueElements;

    int curIndex;

    int Counter;

    int RootNode = 0;

    void Awake()
    {
        dialogue = new ChoiceDialogueState();

    }

    void Start()
    {
        Interact.SetActive(false);
        inBox = false;
        curCharState = dialogue;
        // dialogueNodes = new List<DialogueTree>();
        curIndex = dialogueNodes[RootNode].id;
        DialogueCheckEvent tree = new DialogueCheckEvent(dialogueNodes[RootNode]);
        EventBus.Act(tree);
        //curIndex = DialogueElements[0];
        // Counter = 0;

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
        if (data.curState == DialogueState.NextDialogue)
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
                if (curIndex == x.id) {
                DialogueCheckEvent tree = new DialogueCheckEvent(x);
                EventBus.Act(tree);
                }
                
            }
           
            // if (Counter < DialogueElements.Length)
            // {
            //     curIndex = DialogueElements[Counter];
            //     inBox = true;

            // }

            // else
            // {
            //     curCharState.ChangeState();
            //     curIndex = RepeatDialogueIndex;
            //     inBox = true;

            // }
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
