using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public GameObject PauseMenuUI;

     public string currentScene;

     
  
    static bool isPaused = false;

 
   static bool isDialogueOn = false;


    void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    public static void HideScreen()
    {
        isPaused = false;
        isDialogueOn = true;
    }

    public static void UnHideScreen()
    {
        isDialogueOn = false;
    }

    // Update is called once per frame
    void Update()
    {
         if (!isDialogueOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))  // pauses the game based on the bool, true game is pasued , false game is not paused 
            {
                if (isPaused)
                {
                    
                    Resume();
                }

                else
                {
                    
                   PauseGame();
                }
            }
            
            
        }
    }

     public void Resume()// hides pausemenu, dispalys ingame UI and unpauses game
    { 
         if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound("click");  
        }
        
        PauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;

    }

     void PauseGame()  // pauses game and displays pasue menu 
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


     public void RestartGame() // Restarts the game in the scene 
    {
        SceneManager.LoadScene(currentScene);
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
        }

         if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound("click");  
        }
    }

     public void QuitGame() // quits application 
    {
        SceneManager.LoadScene("MainMenu");
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }

        if (SoundManager.Instance != null)
        {

            SoundManager.Instance.StopMusic("theme");
            SoundManager.Instance.PlaySound("start");
            SoundManager.Instance.PlaySound("click");
            
        }
        
    }
}
