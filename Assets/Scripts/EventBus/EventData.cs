using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class EventData
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float cooled { get; private set; }

    public EventData(float _cooled = 0)
    {
        cooled = _cooled;
    }
}


public class CameraChangeEvent : EventData
{
    public int index { get; private set; }
    public CameraChangeEvent(int index, float _cooled = 0) : base(_cooled)
    {
        this.index = index;
    }
}

public class InputChangeEvent : EventData
{
    public GameObject cam { get; private set; }

    public InputChangeEvent(GameObject _cam, float _cooled = 0) : base(_cooled)
    {
        this.cam = _cam;
    }
}

public class ChangeStateEvent : EventData
{
    public BaseCharacterState state { get; private set; }

    public string name;

    public ChangeStateEvent(BaseCharacterState _state, string _name, float _cooled = 0) : base(_cooled)
    {
        this.state = _state;
        name = _name;
    }
}

public class DialogueEvent : EventData
{
    public string name { get; private set; }
    public int id { get; private set; }

    public DialogueEvent(string _name, int _id, float _cooled = 0) : base(_cooled)
    {
        this.name = _name;
        this.id = _id;
    }
}

public class DialogueSystemEvent : EventData
{
    public NPC id { get; private set; }

    public string text { get; private set; }
    public DialogueSystemEvent(NPC _id, string _text, float _cooled = 0) : base(_cooled)
    {

        this.id = _id;
        text = _text;
    }
}

public class DialogueEndedEvent : EventData
{
    public DialogueState curState { get; private set; }

    public string name; 
     public int id { get; private set; }
    public DialogueEndedEvent(DialogueState _curstate, string _name, int NewId, float _cooled = 0) : base(_cooled)
    {
        curState = _curstate;
        name = _name;
        id = NewId;

    }
}

public class DialogueCheckEvent : EventData
{
    public DialogueTree curState { get; private set; }
     public DialogueCheckEvent(DialogueTree _curstate, float _cooled = 0) : base(_cooled)
    {
        curState = _curstate;

    }
}

public class GiveMoneyEvent : EventData
{

    public int moneyAmount { get; private set; }

    public GiveMoneyEvent(int moneyAmount, float _cooled = 0): base(_cooled) {
     
        this.moneyAmount = moneyAmount;
    
    }



}

public class GameObjectEvent : EventData
{
    public GameObject go;

    public GameObjectEvent(GameObject _go)
    {
        go = _go;
    }
}

public class itemEvent : EventData
{
    public itemSO go;

    public itemEvent(itemSO _go)
    {
        go = _go;
    }
}

public class itemUIEvent : EventData
{
    public itemSO go;

    public int index;

    public itemUIEvent(itemSO _go, int _index)
    {
        go = _go;
        index = _index;
    }
}

public class imageEvent : EventData
{
    public Image go;

    public imageEvent(Image _go)
    {
        go = _go;
    }
}

public class PuzzleEvent : EventData
{
    public Image go;
    public Image go2;

    public PuzzleEvent(Image _go, Image _go2)
    {
        go = _go;
        go2 = _go2;
    }
}

public class ChangeDialogueState : EventData
{

    //public string name
    public DialougeChange change;

    public int index;

    public ChangeDialogueState(DialougeChange _change, int _index)
    {
        change = _change;
        index = _index;
    }


}

public class spriteEvent : EventData
{
    public Sprite go;

    public spriteEvent(Sprite _go)
    {
        go = _go;
    }
}



