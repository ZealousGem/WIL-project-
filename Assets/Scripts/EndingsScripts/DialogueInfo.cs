using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DialogueInfo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      public DialogueData DialogueData = new DialogueData(); // creates a new object to contain the json's file data 

    public IEnumerator LoadData()
    {

        string filepath = Path.Combine(Application.streamingAssetsPath, "Ending.txt"); // will find Json file through the streaming assets folder 

        if (System.IO.File.Exists(filepath)) // checks if the the josn file exisits 
        {
            string DialogueD = System.IO.File.ReadAllText(filepath);

            // uses a couritne to load data so it isn't null errored

            if (!string.IsNullOrEmpty(DialogueD)) // will check if data in josn file isn't null
            {
                DialogueData = JsonUtility.FromJson<DialogueData>(DialogueD); // converts the json data to the variables in dialogueData
                Debug.Log("successfully loaded");
            }

            else
            {
                Debug.Log("Dialogue data not successfully loaded.");
            }

        }

        else
        {
            Debug.Log("File is missing");
        }

        yield return null; // will return null if not file was found 

    }

    private void Awake()
    {
        StartCoroutine(LoadData());   
    }



}


// variables that will be used to catch the data in the JSON File
[System.Serializable]
public class DialogueData
{
    public List<People> Characters; // this is so we can add more than one dialgoue cutscene in the json file
}


[System.Serializable]
public class People
{
    public string id;
    // public string name;
    public List<DialogueNames> character; // list to contain all the names that will be displayed for each character
    public List<DialogueLines> data; // this is made so there can be many dialogue for the character to speak and it can contain many strings instead of one
    
}

[System.Serializable]
public class DialogueNames
{ // this will be the string that will dsiplay the characters name 
    public string name;
}

[System.Serializable]
public class DialogueLines // this be the string element for the dialogue
{
    public string Text;
}

