using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public GameObject startMenu;
    public GameObject OptionsMenu;

   

    public void StartGame()
    {
        SoundManager.Instance.PlaySound("click");
        SoundManager.Instance.StopMusic("start");
        SoundManager.Instance.PlaySound("theme");
        SceneManager.LoadScene("IntroScene");
    }

    public void Options()
    {
        SoundManager.Instance.PlaySound("click");
        startMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void BackToStart()
    {
        SoundManager.Instance.PlaySound("click");
        startMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        SoundManager.Instance.PlaySound("click");
        Application.Quit();
    }
}
