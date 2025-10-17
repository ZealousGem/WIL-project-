using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    
    // public void TryTaxi()
    // {
    //     PlayerStats stats = GameManager.Instance.playerStats;
    //     stats.ChangeTime(5);

    //     if (Random.value < 0.25f)
    //     {
    //         stats.ChangeEnergy(-1);
    //         stats.ChangeMoney(-5);
    //         stats.ChangeTime(-10); // Save time overall
    //         Debug.Log("Taxi Success: Proceed to Point 2");
    //         // Load Point2 scene
    //     }
    //     else
    //     {
    //         Debug.Log("Taxi Failed: Choose a walking route");
    //     }
    // }

    // public void ChooseRoughRoute()
    // {
    //     var stats = GameManager.Instance.playerStats;
    //     stats.ChangeTime(10);

    //     if (Random.value < 0.5f)
    //     {
    //         stats.ChangeEnergy(-1);
    //         stats.ChangeTime(10);
    //         Debug.Log("Gang Encounter: Delayed");
    //     }
    //     else
    //     {
    //         Debug.Log("No Gang Encounter: Proceed");
    //     }
    // }

    // public void ChooseSafeRoute()
    // {
    //     PlayerStats stats = GameManager.Instance.playerStats;
    //     stats.ChangeTime(20);
    //     stats.ChangeEnergy(-2);
    //     Debug.Log("Safe route taken: Proceed to Point 1");
    // }
}
