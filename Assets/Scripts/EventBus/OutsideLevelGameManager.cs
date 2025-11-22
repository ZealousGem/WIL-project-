using System.Collections.Generic;
using UnityEngine;

public class OutsideLevelGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            case PointerArrowTypes.Arrow2:ShowOrHideArrow(Pointers[1].name, data.visibility); break;
            case PointerArrowTypes.Arrow3: ShowOrHideArrow(Pointers[2].name, data.visibility);break;
            case PointerArrowTypes.Arrow4: ShowOrHideArrow(Pointers[3].name, data.visibility); ActivateLockPickPuzzle(); break;
            case PointerArrowTypes.Arrow5: ShowOrHideArrow(Pointers[5].name, data.visibility); BikeEnd.SetActive(true); break;
            case PointerArrowTypes.Arrow6: ShowOrHideArrow(Pointers[4].name, data.visibility);break;
            case PointerArrowTypes.Arrow8: ShowOrHideArrow(Pointers[6].name, data.visibility); TaxiEnd.SetActive(true); break;
            case PointerArrowTypes.CarJackPuzzle: ShowCarJackItems(data.visibility); break;
            case PointerArrowTypes.PuzzleArrows: ItemPickedUp(data.visibility); break;
            case PointerArrowTypes.none: Debug.Log("nothing here"); break;
        }
    }

    int itemCounter = 0;

    public List<GameObject> Pointers;

    public List<GameObject> CarJackObjects;

    public GameObject Pamplhet; 

    public GameObject TaxiEnd;

    public GameObject BikeEnd;

    public CarJackPuzzle craftingTable;

    public LockPuzzle LockPick;

    // Start is called once before the first execution of Update after the MonoBehaviour is create

    void ActivateLockPickPuzzle()
    {
        LockPick.PuzzleCompleted = false;
        Pamplhet.SetActive(true);
        
    }

    void ActivateCraftingPuzzle()
    {
        craftingTable.PuzzleCompleted = false;
    }

    void ItemPickedUp( bool visbilibty)
    {
        itemCounter++;
        if (itemCounter == 2)
        {
            ShowOrHideArrow(Pointers[4].name, visbilibty);
            ActivateCraftingPuzzle();
        }
    }

    void ShowCarJackItems(bool visibility)
    {
         for (int i = 0; i < CarJackObjects.Count; i++)
        {

           CarJackObjects[i].SetActive(visibility);
            
        }
    }
    
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

     void Start()
    {
        for (int i = 0; i < Pointers.Count; i++)
        {
            if (Pointers[i].name != "BeginingArrow")
            {
                Pointers[i].SetActive(false);
            }
        }

        for (int i = 0; i < CarJackObjects.Count; i++)
        {

           CarJackObjects[i].SetActive(false);
            
        }

        BikeEnd.SetActive(false);
        TaxiEnd.SetActive(false);
        Pamplhet.SetActive(false);
    }
}
