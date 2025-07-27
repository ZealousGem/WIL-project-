using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Resources")]
    public float resourceA = 100f;
    public float resourceB = 100f;

    [Header("UI")]
    public TMP_Text resourceAText;
    public TMP_Text resourceBText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UpdateUI();
    }

    public void SpendResources(float costA, float costB)
    {
        resourceA -= costA;
        resourceB -= costB;

        resourceA = Mathf.Max(0, resourceA);
        resourceB = Mathf.Max(0, resourceB);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (resourceAText != null)
            resourceAText.text = $"Energy: {(int)resourceA:F1}";

        if (resourceBText != null)
            resourceBText.text = $"Money: {(int)resourceB:F1}";
    }
}

