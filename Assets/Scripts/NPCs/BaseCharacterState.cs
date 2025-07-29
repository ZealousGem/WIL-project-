
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;

public abstract class BaseCharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public abstract void EnterState(string name, int curIndex);

  

    public abstract void ChangeState();
}


public enum DialogueState
{
    Ended,
    NextDialogue
    
}

public class ChoiceDialogueState : BaseCharacterState
{


  

    public override void ChangeState()
    {
        Debug.Log("changing state D");
       // OnDisable();
        RepeatDialogueState repeatDialogue = new RepeatDialogueState();
        ChangeStateEvent newState = new ChangeStateEvent(repeatDialogue);
        EventBus.Act(newState);
    }

   

    public override void EnterState(string name, int curIndex)
    {
        // throw new System.NotImplementedException();
        
        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
     




    }


}



public class RepeatDialogueState : BaseCharacterState
{
    public override void ChangeState()
    {
        // throw new System.NotImplementedException();
       // Debug.Log("changing state");
    }


    public override void EnterState(string name, int curIndex)
    {
        // throw new System.NotImplementedException();
        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
       // Debug.Log("entering state");
    }
}



