using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int time = 0;       // 0–100
    public int energy = 5;     // 0–5
    public float money = 0f;   // R0–R10

    public void ChangeTime(int amount)
    {
        time += amount;
    }

    public void ChangeEnergy(int amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, 5);
    }

    public void ChangeMoney(float amount)
    {
        money = Mathf.Clamp(money + amount, 0f, 10f);
    }
    
}
