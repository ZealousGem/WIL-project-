
using UnityEngine;

public abstract class BaseCharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public abstract void EnterState(string name, int curIndex);

  

    public abstract void ChangeState(string name);
}


public enum DialogueState
{
    Ended,
    NextDialogue
    
}

public class ChoiceDialogueState : BaseCharacterState
{


  

    public override void ChangeState(string name)
    {
      
        RepeatDialogueState repeatDialogue = new RepeatDialogueState();
        ChangeStateEvent newState = new ChangeStateEvent(repeatDialogue, name);
        EventBus.Act(newState);
    }

   

    public override void EnterState(string name, int curIndex)
    {
      
        
        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
     




    }


}



public class RepeatDialogueState : BaseCharacterState
{
    public override void ChangeState(string name)
    {
        
    }


    public override void EnterState(string name, int curIndex)
    {
        ;
        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
     
    }
}



