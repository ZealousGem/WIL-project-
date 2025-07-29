using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

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

    public ChangeStateEvent(BaseCharacterState _state, float _cooled = 0) : base(_cooled)
    {
        this.state = _state;
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
     public int id { get; private set; }
    public DialogueEndedEvent(DialogueState _curstate, int NewId, float _cooled = 0) : base(_cooled)
    {
        curState = _curstate;
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
