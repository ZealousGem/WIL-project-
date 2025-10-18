using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialoguePanel;
    public GameObject button;
    public TMP_Text DialogueText;
    public TMP_Text titleUI;
    public Image image;
    public string[] dialouge;
    public Sprite[] images;

    public string title;
    private int element;
    private bool PlayerInView = false;
    public float Speed;

    public bool StartTutoruail = false;

    public string tags;

    
    // Update is called once per frame
    void Update()
    {
        if (PlayerInView || StartTutoruail)
        {
            if (Input.GetKeyDown(KeyCode.E) || StartTutoruail || Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 0f;
                ActivateTutorial();

            }
        }

    }
    
    void ActivateTutorial()
    {
            Button but = button.GetComponent<Button>();
            but.onClick.AddListener(NextLine);
            dialoguePanel.SetActive(true);
            button.SetActive(false);
            PlayerInView = false;
            StartTutoruail = false;
            titleUI.text = title;
            StartCoroutine(TypeDialoguew());
            UpdateImage();
    }

    public void ActivateButton()
    {
        NextLine();
    }

    public void UpdateImage()
    {
        image.sprite = images[element];
        
    }

    public void NextLine()
    {
        
        button.SetActive(false);
        if (element < dialouge.Length - 1)
        {
            element++;
            DialogueText.text = "";
            StartCoroutine(TypeDialoguew());
            UpdateImage();
        }

        else
        {
            noText();
            PlayerInView = false;
            

        }
    }

    public void noText()
    {
        DialogueText.text = "";
        element = 0;
        Time.timeScale = 1f;
        dialoguePanel.SetActive(false);
        List<GameObject> shiftObjects = new List<GameObject>();
        Button but = button.GetComponent<Button>();
        but.onClick.RemoveListener(NextLine);
        shiftObjects.AddRange(GameObject.FindGameObjectsWithTag(tags));

        foreach (GameObject n in shiftObjects)
        {
            TutorialScript puzzle = n.GetComponent<TutorialScript>();
            if (puzzle != null)
            {
                puzzle.enabled = false;
            }
        }
        
    }

    IEnumerator TypeDialoguew()
    {
        foreach (var dial in dialouge[element].ToCharArray())
        {

            DialogueText.text += dial;
            yield return new WaitForSecondsRealtime(Speed);

        }

        button.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
     if (other.CompareTag("Player"))
        {
            
            PlayerInView = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerInView = false;
    }


}