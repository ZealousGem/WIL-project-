
using UnityEngine;

public abstract class BaseCharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void EnterState();

    public abstract void ChangeState();
}


public class ChoiceDialogueState : BaseCharacterState
{


    public override void ChangeState()
    {
        // throw new System.NotImplementedException();
        Debug.Log("changing state D");
        RepeatDialogueState repeatDialogue = new RepeatDialogueState();
        ChangeStateEvent newState = new ChangeStateEvent(repeatDialogue);
        EventBus.Act(newState);
      

    }

    public override void EnterState()
    {
        // throw new System.NotImplementedException();
        Debug.Log("entering state D");
        ChangeState();
          
    }
}



public class RepeatDialogueState : BaseCharacterState
{
    public override void ChangeState()
    {
        // throw new System.NotImplementedException();
        Debug.Log("changing state");
    }

    public override void EnterState()
    {
        // throw new System.NotImplementedException();
        Debug.Log("entering state");
    }
}



