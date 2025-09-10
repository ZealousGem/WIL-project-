using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Characters Characters = new Characters();
    public Name curCharacter = new Name();

    string CurName, CurId;

    void OnEnable()
    {
        EventBus.Subscribe<DialogueEvent>(PassingData);
    }

    void OnDisable()
    {
        EventBus.Subscribe<DialogueEvent>(PassingData);
    }

    public void PassingData(DialogueEvent data)
    {
        // Debug.Log("huh");
        SetName(data.name);
        SetID(data.id);
        StartCoroutine(LoadData());
        Debug.Log(data.id + data.name);

    }

    string SetName(string name)
    {
      //  Debug.Log(name); 
        this.CurName = name;
        return CurName;
    }

    string SetID(int _id)
    {
       //  Debug.Log(_id); 
        this.CurId = _id.ToString();
        return CurId;
    }



    public IEnumerator LoadData()
    {
        string filepath = Path.Combine(Application.streamingAssetsPath, "Dialogue.txt");

        if (File.Exists(filepath))
        {
            string tempData = File.ReadAllText(filepath);

            if (!string.IsNullOrEmpty(tempData))
            {
                Characters = JsonUtility.FromJson<Characters>(tempData);
                FindCharacter(Characters);

            }

            else
            {
                Debug.Log("file not found");
            }

        }
        yield return null;
    }


    Name FindCharacter(Characters Temp)
    {
        if (Temp != null)
        {
            foreach (Name tempChar in Temp.id)
            {
                if (tempChar.name == CurName)
                {
                    curCharacter = tempChar;
                   


                }              

            }
            ActivateDialogueSystem(curCharacter, CurId);
            Characters = null;
            CurId = null;
            name = null;

            return curCharacter;

        }

        else
        {
             Debug.Log("null here" + CurName);
            return null;

        }

    }

    void ActivateDialogueSystem(Name _DialogueIndex, string _curId)
    {
      
        foreach (NPC scene in _DialogueIndex.dialogue)
        {
            if (_curId == scene.id)
            {
               // Debug.Log("working");
                NPC temp = scene;
                DialogueSystemEvent t = new DialogueSystemEvent(temp, CurName);
                EventBus.Act(t);
            }
        }
       

    }
}




[System.Serializable]
public class Characters
{
    public List<Name> id;


}

[System.Serializable]

public class Name {

    public string name;
    public List<NPC> dialogue;
}

[System.Serializable]
public class NPC
{
    public string id;
    public List<Dialogue> Text;
}


[System.Serializable]
public class Dialogue
{
    public string words;
}


