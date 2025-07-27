using UnityEngine;
using System;
using System.Collections.Generic;

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
