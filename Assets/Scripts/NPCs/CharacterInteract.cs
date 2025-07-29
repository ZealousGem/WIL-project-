using System.Data;
using TMPro;
using Unity.VisualScripting;
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
    string NPCname;

     [SerializeField]
     int RepeatDialogueIndex;

     [SerializeField]
    int[] DialogueElements;

    int curIndex;

    int Counter;

    void Awake()
    {
        dialogue = new ChoiceDialogueState();
       
    }

    void Start()
    {
        Interact.SetActive(false);
        inBox = false;
        curCharState = dialogue;
        curIndex = DialogueElements[0];
        Counter = 0;
       
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
         if (data.curState == DialogueState.NextDialogue) {
            Counter++;
            if (Counter < DialogueElements.Length)
            {
                curIndex = curIndex + DialogueElements[Counter];
                inBox = true;

            }

            else
            {
                curCharState.ChangeState();
                curIndex = RepeatDialogueIndex;
                inBox = true;

            }
         }
        
       
     }

    // Update is called once per frame
    void Update()
    {
        if (inBox) {
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
