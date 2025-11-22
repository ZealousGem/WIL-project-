using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
   
    public DisplaySystem start;
    public string Disname;
    public DialogueInfo info;
    public string Nextscene;
    public string nextSong;
    void Start()
    {
       
        if (info != null)
        {
            StartCoroutine(LoadDialogue());// creates a couritnne so data can load before dialogue is implemented
        }
        else
        {
            Debug.Log("info not assigned");
        }


    }

    // Update is called once per frame

   public IEnumerator LoadDialogue()
    {

        ;
        yield return StartCoroutine(info.LoadData());
        if (info.DialogueData.Characters != null || info.DialogueData.Characters.Count > 0) // will check if data is instatiated
        {
            StartCoroutine(Dialogue()); // will only start the dialogue sequence once all the data from the json file has been transfered 
        }
        else
        {
            Debug.Log("data still not loaded");
        }


    } 

    public IEnumerator Dialogue()
    {
       

            start.StartDialogue(Disname);
           
            while (!start.end) // if the end bool is still false this will freeze the loop so the Queue can finish in DialogueSystem
            {
                yield return null;
            }
            start.end = true;

           // SkipDialogue(); // will go to the next scene once dialogue cutscene has finished 
        



    }

    public void SkipDialogue()
    {

        SceneManager.LoadScene(Nextscene);
    }

}
