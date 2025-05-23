using UnityEngine;

public class Phase3Manager : MonoBehaviour
{
   
    public void BuyFood()
    {
        PlayerStats stats = GameManager.Instance.playerStats;
        if (stats.money >= 5 && stats.time <= 95)
        {
            stats.ChangeMoney(-5);
            stats.ChangeTime(5);
            
            stats.ChangeEnergy(1);
            Debug.Log("Food bought");
        }
        else
        {
            Debug.Log("Not enough money or time");
        }
    }

    public void HelpAtStall()
    {
        PlayerStats stats = GameManager.Instance.playerStats;
        stats.ChangeTime(10);
        stats.ChangeMoney(5);
        
    }

    public void DubiousErrand()
    {
        PlayerStats stats = GameManager.Instance.playerStats;
        if (Random.value < 0.5f)
        {
            stats.ChangeTime(10);
            stats.ChangeMoney(10);
            
        }
        else
        {
            GameManager.Instance.GameOver("Caught doing something illegal");
        }
    }

    public void IgnoreAndMoveOn()
    {
        // Do nothing
        Debug.Log("You chose to ignore the stall and moved on");
    }
}
