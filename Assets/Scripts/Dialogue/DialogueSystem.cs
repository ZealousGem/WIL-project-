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


public class DialogueSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Queue<string> lines;
    // private Queue<Sprite> images;
    //private Queue<string> names;
    private string names;
    public GameObject Dialogue;
    public GameObject Button;
    // public Image image;

   
    public TMP_Text Charname;
    public bool isAutomatic;
    public TMP_Text description;
    public int counter = 0;
    public bool end;
    public float Speed;

    void OnEnable()
    {
        EventBus.Subscribe<DialogueSystemEvent>(StartDialogue);
    }

    void OnDisable()
    {
        EventBus.Subscribe<DialogueSystemEvent>(StartDialogue);
    }

    void Start()
    {
        Dialogue.SetActive(false);
        lines = new Queue<string>();
        names = "";
        end = true;
    }

    void StartDialogue(DialogueSystemEvent dia)
    {
        Dialogue.SetActive(true);
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
    }
    // Update is called once per frame
    public void DisplayNextSentence()
    {
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
        names = "";
        Dialogue.SetActive(false);
       
       try
        {
        DialogueEndedEvent ending = new DialogueEndedEvent(DialogueState.NextDialogue);
        EventBus.Act(ending);
        }
        catch { }
        
    }

}
