using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TextCore.Text;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using Flexalon;



public class DialogueSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Queue<string> lines;
    // private Queue<Sprite> images;
    //private Queue<string> names;
    [SerializeField]
    public DialogueTree tree;

    private string names;

    List<int> answers = new List<int>();

    public GameObject Dialogue;
    public GameObject Button;

    public GameObject ChoiceButton;

    // public Image image;
    public TMP_Text[] ButtonText;

    public TMP_Text Charname;
    public bool isAutomatic;
    public TMP_Text description;
    public int counter = 0;
    public bool end;
    public float Speed;

    

    void OnEnable()
    {
        EventBus.Subscribe<DialogueSystemEvent>(StartDialogue);
        EventBus.Subscribe<DialogueCheckEvent>(GetNextId);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<DialogueSystemEvent>(StartDialogue);
        EventBus.Unsubscribe<DialogueCheckEvent>(GetNextId);
    }

    void Start()
    {
        ChoiceButton.SetActive(false);
        Dialogue.SetActive(false);
        lines = new Queue<string>();
        names = "";
        end = true;
       
    }

   public void ChangeTree(DialogueTree _tree)
    {
        tree = _tree;
    }

    IEnumerator UIAnimation(GameObject _Dialogue)
    {
        CanvasGroup cock = _Dialogue.GetComponent<CanvasGroup>();
        cock.alpha = 0f;
        float counter = 0f;
        float maxCounter = 1f;
        while (counter < maxCounter)
        {

            counter += Time.deltaTime * 2;
            cock.alpha = Mathf.Lerp(0, 1, counter / maxCounter);
             yield return null;
            

           
        }

        cock.alpha = 1f;
    }

    IEnumerator ButtonUIAnimation(GameObject _Button)
    {
        CanvasGroup cock = _Button.GetComponent<CanvasGroup>();
        cock.alpha = 0f;
        float counter = 0f;
        float maxCounter = 0.3f;
        RectTransform T = _Button.GetComponent<RectTransform>();
        Vector3 StartSc = Vector3.zero;
        Vector3 EndSc = Vector3.one;
        T.localScale = StartSc;
        yield return null;

        while (counter < maxCounter)
        {

            counter += Time.deltaTime;
           float transforming = Mathf.SmoothStep(0, 1, counter/ maxCounter);
            cock.alpha = transforming;
            T.localScale = Vector3.Lerp(StartSc, EndSc, transforming);
            yield return null;



        }

        cock.alpha = 1f;
        T.localScale = EndSc;
    }

    void StartDialogue(DialogueSystemEvent dia)
    {
        Dialogue.SetActive(true);
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.enabled = false;
        StartCoroutine(UIAnimation(Dialogue));
        end = false;
        NPC Chart = dia.id;
        names = dia.text;
        if (Chart != null)
        {
            lines.Clear();


            foreach (Dialogue temp in Chart.Text)
            {
                lines.Enqueue(temp.words);
            }
            DisplayNextSentence();
        }

        else
        {
            Debug.Log("not found");
        }
        
         if (tree == null)
         {
            Debug.Log("tree is null");    
        }
    }
    // Update is called once per frame
    public void DisplayNextSentence()
    {
         if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("ps1");
            }
        if (lines.Count == 0) // if all the elements have been dequed the dailogue will end
        {
            EndDialogue();
            return; // returns method
        }
        string sentence = lines.Dequeue(); // everytime the diplsay is pressed the element in front will be removed and makes the element behind in fron of the Queue
                                           //  Sprite Nextimage = images.Dequeue();
        string CharName = names;
        Charname.text = CharName;
        // image.sprite = Nextimage;
        StartCoroutine(TypeDialogue(sentence));
        Button.SetActive(false);
        counter += 1;

    }

    public IEnumerator TypeDialogue(string sentence)
    {
        
        description.text = "";
        foreach (var T in sentence.ToCharArray()) // will loop the text string through it's characters
        {
            description.text += T; // will dsiplay each character at a certain timed update to create dialogue text animation 
            yield return new WaitForSeconds(0.03f);
         
          
        }
         
        if (isAutomatic) // bool will check if the dialogue is an automatic for the button not to be displayed
        {
            yield return new WaitForSeconds(2f);
            AutomaticClick();
        }

        else // will be displayed for player to progress to next element in queue if bool is false 
        {
            Button.SetActive(true);
        }

    }

    public void AutomaticClick()
    {
        DisplayNextSentence();
    }

    void EndDialogue() // will end the dialogue by setting bool to false allowing the for loop in dialogue manager to move the i to the next position
    {
        Charname.text = "";
        description.text = "";
        end = true;
        counter = 0;
        //  images.Clear();

        if (tree == null) {
          Debug.Log("tree is null");
       }
         //Debug.Log(tree.Choices);
        Dialogue.SetActive(false);
        if (tree.Choices.Count > 1)
        {

            // blank

            answers.Clear();
            ShowButtons();
            for (int i = 0; i < tree.Choices.Count; i++)
            {

                answers.Add(tree.Choices[i].Choice);
                ButtonText[i].text = tree.Choices[i].answer;


            }


        }

        else
        {


            GiveAmount(0);
            try
            {
                DialogueEndedEvent ending = new DialogueEndedEvent(DialogueState.NextDialogue, names, tree.Choices[0].Choice);
                EventBus.Act(ending);
                //  Debug.Log(names);
                names = "";
                //tree = null;
            }
            catch { }
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.enabled = true;
        }
        

    }

    void GiveAmount(int index)
    {
         if (tree != null)
            {

                if (tree.Choices[index].amount > 0)
                {
                    int GiveAmount = tree.Choices[index].amount;
                    GiveMoneyEvent transfer = new GiveMoneyEvent(GiveAmount);
                    EventBus.Act(transfer);
                }

            if (tree.Choices[index].eventChanged != null && tree.Choices[index].eventChanged != "")
            {
                Debug.Log(tree.Choices[index].eventChanged + "we have an answer");
            }

            if (tree.Choices[index].item != null)
            {
                Debug.Log(tree.Choices[index].item + "we have found an item");
                itemEvent it = new itemEvent(tree.Choices[index].item);
                EventBus.Act(it);

            }
                
            if (tree.Choices[index].changeObjective != PointerArrowTypes.none)
            {
                Debug.Log("change objective");
                PointerEvent pointer = new PointerEvent(tree.Choices[index].changeObjective, tree.Choices[index].hidePointer);
                EventBus.Act(pointer);
            }

            }
    }

   public void Answer1()
    {
        if (answers != null)
        {
            if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("ps1");
            }
            ChoiceButton.SetActive(false);
            GiveAmount(0);
            DialogueEndedEvent ending = new DialogueEndedEvent(DialogueState.Ended, names, answers[0]);
            EventBus.Act(ending);

            
            //   tree = null;
        }
    }

   public void Answer2()
    {
        if (answers != null)
        {

           if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("ps1");
            }
            ChoiceButton.SetActive(false);
            GiveAmount(1);
            DialogueEndedEvent ending = new DialogueEndedEvent(DialogueState.Ended, names, answers[1]);
            EventBus.Act(ending);
             
            //tree = null;
        }
    }

    void ShowButtons()
    {
        ChoiceButton.SetActive(true);
        StartCoroutine(ButtonUIAnimation(ChoiceButton));
    }

    void GetNextId(DialogueCheckEvent data)
    {
        if (data != null)
        {
            tree = new DialogueTree();
            tree = data.curState;
        }

        else
        {
            Debug.Log("tree is null");
        }
     
       // inNextId 
    }

}
