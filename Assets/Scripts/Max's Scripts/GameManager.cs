using System.Collections.Generic;
using UnityEngine;

public enum PointerArrowTypes
{
    none,
    Arrow1,
    PuzzleArrows,

    Arrow2,

    Arrow3,

    Arrow4,

    Arrow5,


}

public class GameManager : MonoBehaviour
{

    void OnEnable()
    {
       EventBus.Subscribe<PointerEvent>(FindType);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<PointerEvent>(FindType);
    }

    void FindType(PointerEvent data)
    {
        switch (data.Arrow)
        {
            case PointerArrowTypes.Arrow1: ShowOrHideArrow(Pointers[0].name, data.visibility); break;
            case  PointerArrowTypes.Arrow2:ShowOrHideArrow(Pointers[1].name, data.visibility); break;
            case  PointerArrowTypes.Arrow3: ShowOrHideArrow(Pointers[2].name, data.visibility);break;
            case  PointerArrowTypes.Arrow4: ShowOrHideArrow(Pointers[3].name, data.visibility);break;
            case  PointerArrowTypes.Arrow5: ShowOrHideArrow(Pointers[4].name, data.visibility);break;
            case  PointerArrowTypes.PuzzleArrows: ShowOrHideArrow(Pointers[1].name, data.visibility);  ShowOrHideArrow(Pointers[2].name, data.visibility); ShowOrHideArrow(Pointers[3].name, data.visibility);break;
            case PointerArrowTypes.none: Debug.Log("nothing here"); break;
        }
    }

    int PuzzleCounter = 0;

    public List<GameObject> Pointers;

    // Start is called once before the first execution of Update after the MonoBehaviour is create

   
    
    void ShowOrHideArrow(string ArrowName, bool condition)
    {
        for (int i = 0; i < Pointers.Count; i++)
        {
            if (ArrowName == Pointers[i].name)
            {
                if (condition)
                {
                     Pointers[i].SetActive(true);
                }
                
                else
                {
                     Pointers[i].SetActive(false);
                }
               
            }
        }
    }


    public void PuzzleCompleted(int count)
    {
        PuzzleCounter += count;
        Debug.Log("completed");
        if (PuzzleCounter == 3)
        {
            ChangeDialogueState change = new ChangeDialogueState(DialougeChange.TutorialPuzzle, 1);
            EventBus.Act(change);
            ShowOrHideArrow(Pointers[0].name, true);

        }
    }

    void Start()
    {
        for (int i = 0; i < Pointers.Count; i++)
        {
            if (Pointers[i].name != "BeginingArrow")
            {
                Pointers[i].SetActive(false);
            }
        }
    }


    public void GameOver(string reason)
    {
        //playerStats.isGameOver = true;
        Debug.Log("Game Over: " + reason);
        // Load end screen or show UI
    }


}
