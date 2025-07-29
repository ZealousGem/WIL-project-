using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class DialogueChoice
{
    public int Choice;

    public string answer;
}

[System.Serializable]
public class DialogueTree
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int id;

    [SerializeField]
    public List<DialogueChoice> Choices = new List<DialogueChoice>();
    

    
    

   
}
