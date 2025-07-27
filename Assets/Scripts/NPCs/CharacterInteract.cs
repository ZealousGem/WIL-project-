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
    int DialogueIndex;

     [SerializeField]
    int RepeatDialogueIndex;

    void Awake()
    {
        dialogue = new ChoiceDialogueState();
       
    }

    void Start()
    {
        Interact.SetActive(false);
        inBox = false;
        curCharState = dialogue;
    }

    void OnEnable()
    {
         EventBus.Subscribe<ChangeStateEvent>(SwitchState);
    }

    void OnDisable()
    {
         EventBus.Unsubscribe<ChangeStateEvent>(SwitchState);
    }

    // Update is called once per frame
    void Update()
    {
        if (inBox) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                curCharState.EnterState();
                
                
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
