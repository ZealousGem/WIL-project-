using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class DialogueChoice
{
    public int Choice;

    public string answer;

    public int amount;

    public itemSO item;

    public PointerArrowTypes changeObjective;

    public bool hidePointer;

    public Sprite TakeItem;

    public DialougeChange eventChanged;

    public int DialogueStateIndex;
}

[System.Serializable]
public class DialogueTree
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int id;

    [SerializeField]
    public List<DialogueChoice> Choices = new List<DialogueChoice>();
    

    
    

   
}
