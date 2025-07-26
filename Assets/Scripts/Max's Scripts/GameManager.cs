using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerStats = new PlayerStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(string reason)
    {
        //playerStats.isGameOver = true;
        Debug.Log("Game Over: " + reason);
        // Load end screen or show UI
    }


}
