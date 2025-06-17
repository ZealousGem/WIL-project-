using UnityEngine;
using UnityEngine.UI;

public class HoverInfoDisplay : MonoBehaviour
{
    [Header("Layer Settings")]
    public string hoverLayer = "Outline";
    private int originalLayer;

    [Header("UI Settings")]
    public GameObject infoUI; // Assign a world-space canvas or UI prefab in the inspector
    public Vector3 uiOffset = new Vector3(0, 1f, 0); // Offset above the object

    private bool isHovered = false;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        originalLayer = gameObject.layer;

        if (infoUI != null)
            infoUI.SetActive(false);
    }

    private void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHovered)
                    OnHoverEnter();
                UpdateUIPosition();

                if (Input.GetMouseButtonDown(0))
                {
                    ResourceManager.Instance.SpendResources(10,10);
                }
            }
            else if (isHovered)
            {
                OnHoverExit();
            }
        }
        else if (isHovered)
        {
            OnHoverExit();
        }
    }

    void OnHoverEnter()
    {
        isHovered = true;
        gameObject.layer = LayerMask.NameToLayer(hoverLayer);

        if (infoUI != null)
            infoUI.SetActive(true);
    }

    void OnHoverExit()
    {
        isHovered = false;
        gameObject.layer = originalLayer;

        if (infoUI != null)
            infoUI.SetActive(false);
    }

    void UpdateUIPosition()
    {
        if (infoUI != null)
        {
            infoUI.transform.position = transform.position + uiOffset;

            Vector3 camDirection = infoUI.transform.position - mainCam.transform.position;
            infoUI.transform.rotation = Quaternion.LookRotation(camDirection);
        }
    }
}

