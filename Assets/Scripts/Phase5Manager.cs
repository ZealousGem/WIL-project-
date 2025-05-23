using UnityEngine;

public class Phase5Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EvaluateArrival()
    {
        int time = GameManager.Instance.playerStats.time;

        if (time < 30)
        {
            Debug.Log("Very Early: Vulnerable, energy drop");
        }
        else if (time < 45)
        {
            Debug.Log("Early Arrival: Best Outcome");
        }
        else if (time < 60)
        {
            Debug.Log("On Time: Neutral");
        }
        else if (time < 80)
        {
            Debug.Log("Rushed Late: Risky");
        }
        else
        {
            GameManager.Instance.GameOver("You were too late for school.");
        }
    }
}
