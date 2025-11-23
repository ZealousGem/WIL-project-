using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TextCore.Text;
using System.IO;
using TMPro;
using NUnit.Framework.Constraints;

public class DisplaySystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 private Queue<string> lines;
   // private Queue<Sprite> images;
    private Queue<string> names;

    public GameObject Dialogue;
    public GameObject Button;
   // public Image image;

     List<string> login =new List<string>();
    public TMP_Text Charname;
    public bool isAutomatic;
    public TMP_Text description;
    public int counter = 0;
    public bool end;
    public float Speed;
    DialogueInfo info;

    private void Start()
    {
        Dialogue.SetActive(false);
        lines = new Queue<string>(); // creates a Queue string for the dialogue 
     //   images = new Queue<Sprite>(); // creates Queue sprite for images
        names = new Queue<string>(); // creates Queue string for the names 
        end = true;

        if (info == null)
        {
            info = FindAnyObjectByType<DialogueInfo>(); // instiates the inforamtion from the Json file


        }

        if (info == null)
        {
            Debug.LogError("DialogueInfo component is not assigned or not found in the scene.");
        }
    }

    public void StartDialogue(string ChacterName) // method that will start the dialogue using the characters name in the string array from diaslogue manager
    {
       
        
        Debug.Log("dialogue starts");
        Dialogue.SetActive(true);
        end = false;
        People characterD = info.DialogueData.Characters.Find(Characters => Characters.id == ChacterName); // this will create a character object and will instatite with the data from the josn file
        if (characterD != null)
        {


            Debug.Log("Successfully loaded characters.");

            lines.Clear();
          //  images.Clear();
            names.Clear();

            // clears any elements that are in the queue

            foreach (DialogueLines sent in characterD.data)
            {
                lines.Enqueue(sent.Text); // will add all the text elements from object into the queue to create a sequential order
            }

           

            foreach (DialogueNames sent in characterD.character)
            {
                names.Enqueue(sent.name);
            }

            DisplayNextSentence(); // this will activate to start the elemtns to be removed from the queue
        }

        else
        {
            Debug.Log("character not found");
        }





    }

   

    public void AutomaticClick()
    {
        DisplayNextSentence();
    }

    public void DisplayNextSentence() // this method is activated by the next button input
    {
       
            //Debug.Log("next dialogue");
            if (lines.Count == 0) // if all the elements have been dequed the dailogue will end
        {
            EndDialogue();
            return; // returns method
        }
        string sentence = lines.Dequeue(); // everytime the diplsay is pressed the element in front will be removed and makes the element behind in fron of the Queue
      //  Sprite Nextimage = images.Dequeue();
        string CharName = names.Dequeue();


        Charname.text = CharName;
       // image.sprite = Nextimage;
        Button.SetActive(false);
        StartCoroutine(TypeDialogue(sentence)); // displays the dialogue text in a courtieine to have text pop up in a timed sequence 
        login.Add(sentence);
        counter += 1; // keeps track fo the front position element in the queue

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

    void EndDialogue() // will end the dialogue by setting bool to false allowing the for loop in dialogue manager to move the i to the next position
    {
        Charname.text = "";
        description.text = "";
        end = true;
        counter = 0;
        login.Clear();
      //  images.Clear();
        names.Clear();
        Dialogue.SetActive(false);
    }
}
