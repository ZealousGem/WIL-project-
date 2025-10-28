using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.UIElements;

[System.Serializable]
public class TutorialImages
{
    public Sprite staticimage;

    public AnimationClip Animation;

    public RuntimeAnimatorController Controler; 

    public VisualType type;
}

public enum VisualType {
        StaticSprite,
        AnimatedGIF
    }

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialoguePanel;
    public GameObject button;
    public TMP_Text DialogueText;
    public TMP_Text titleUI;
    public Image image;
    public string[] dialouge;
    public TutorialImages[] images;

    public string title;
    private int element;
    private bool PlayerInView = false;
    public float Speed;

    public bool StartTutoruail = false;

    public string tags;

    Animator imageAnimator;


    // Update is called once per frame
    void Update()
    {
        if (PlayerInView || StartTutoruail)
        {
            if (Input.GetKeyDown(KeyCode.E) || StartTutoruail)
            {
                Time.timeScale = 0f;
                ActivateTutorial();

            }
        }

    }

    void ActivateTutorial()
    {
         if (SoundManager.Instance != null)
         {
             SoundManager.Instance.PlaySound("ps1");
         }
        imageAnimator = image.GetComponent<Animator>();
        if (imageAnimator != null)
        {
            // CRITICAL: Set Update Mode to Unscaled Time so it plays when Time.timeScale is 0
            imageAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            imageAnimator.enabled = false; // Disable it initially
        }
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
        
        if (VisualType.StaticSprite == images[element].type)
        {
            image.sprite = images[element].staticimage;
        }

        else if (VisualType.AnimatedGIF == images[element].type)
        {
            imageAnimator.runtimeAnimatorController = images[element].Controler;
            imageAnimator.enabled = true;
            imageAnimator.Play(images[element].Animation.name);
        }
        
        else
        {
            image.sprite = null;
        }
       

    }

    public void NextLine()
    {
        if (SoundManager.Instance != null)
            {
             SoundManager.Instance.PlaySound("ps1");
            }
        button.SetActive(false);
        if (element < dialouge.Length - 1)
        {
            element++;
            DialogueText.text = "";
        if (imageAnimator != null) imageAnimator.enabled = false;
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
        if (imageAnimator != null) imageAnimator.enabled = false;

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