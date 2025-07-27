
using UnityEditor.Rendering;
using UnityEngine;

public abstract class BaseCharacterState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public abstract void EnterState(string name, int curIndex);

  

    public abstract void ChangeState();
}


public enum DialogueState
{
    Ended
    
}

public class ChoiceDialogueState : BaseCharacterState
{


     void OnEnable()
    {
        EventBus.Subscribe<DialogueEndedEvent>(EndDialogue);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<DialogueEndedEvent>(EndDialogue);
    }

    public override void ChangeState()
    {
        Debug.Log("changing state D");
        OnDisable();
        RepeatDialogueState repeatDialogue = new RepeatDialogueState();
        ChangeStateEvent newState = new ChangeStateEvent(repeatDialogue);
        EventBus.Act(newState);
    }

    public void EndDialogue(DialogueEndedEvent state)
    {
        //   throw new System.NotImplementedException();
        if (state.curState == DialogueState.Ended) {
            ChangeState();
         }
        //  ChangeState(name, curIndex);
    }

    public override void EnterState(string name, int curIndex)
    {
        // throw new System.NotImplementedException();

        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
        OnEnable();
        // Debug.Log("entering state D");




    }


}



public class RepeatDialogueState : BaseCharacterState
{
    public override void ChangeState()
    {
        // throw new System.NotImplementedException();
        Debug.Log("changing state");
    }


    public override void EnterState(string name, int curIndex)
    {
        // throw new System.NotImplementedException();
        DialogueEvent setDialogue = new DialogueEvent(name, curIndex);
        EventBus.Act(setDialogue);
        Debug.Log("entering state");
    }
}



