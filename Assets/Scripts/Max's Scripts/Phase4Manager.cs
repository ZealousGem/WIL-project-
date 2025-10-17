using UnityEngine;

public class Phase4Manager : MonoBehaviour
{
   
    // public void RiskyDetour()
    // {
    //     float outcome = Random.value;
    //     PlayerStats stats = GameManager.Instance.playerStats;

    //     if (outcome < 0.33f)
    //     {
    //         stats.ChangeTime(-5);
    //         Debug.Log("Shortcut successful");
    //     }
    //     else if (outcome < 0.66f)
    //     {
    //         stats.ChangeTime(10);
    //         stats.ChangeEnergy(-1);
    //         //stats.ChangeMorality(-1);
    //         Debug.Log("Complication on risky path");
    //     }
    //     else
    //     {
    //         stats.ChangeTime(15);
    //         Debug.Log("Dead end, lost time");
    //     }
    // }

    // public void SafeDetour()
    // {
    //     PlayerStats stats = GameManager.Instance.playerStats;
    //     if (Random.value < 0.5f)
    //     {
    //         stats.ChangeTime(15);
    //         Debug.Log("Safe path, just slow");
    //     }
    //     else
    //     {
    //         stats.ChangeTime(20);
    //         Debug.Log("Held up by crowd");
    //     }
    // }

    // public void WaitItOut()
    // {
    //     PlayerStats stats = GameManager.Instance.playerStats;
    //     float chance = Random.value;

    //     if (chance < 0.33f)
    //     {
    //         stats.ChangeTime(5);
    //         Debug.Log("Road cleared quickly");
    //     }
    //     else if (chance < 0.66f)
    //     {
    //         stats.ChangeTime(15);
    //         Debug.Log("Longer wait than expected");
    //     }
    //     else
    //     {
    //         stats.ChangeTime(15);
    //         Debug.Log("Forced to detour after waiting");
    //     }
    // }
}
