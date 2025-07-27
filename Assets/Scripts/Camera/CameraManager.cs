using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    List<GameObject> CamerasinScene = new List<GameObject>();

    void OnEnable()
    {
        EventBus.Subscribe<CameraChangeEvent>(ChangingCameras);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<CameraChangeEvent>(ChangingCameras);
    }

     void ChangingCameras(CameraChangeEvent data)
    {

        for (int i = 0; i < CamerasinScene.Count; i++) {
            GameObject cam = CamerasinScene[i];
            if (i == data.index)
            {

                cam.SetActive(true);
                InputChangeEvent raycastCam = new InputChangeEvent(cam);
                EventBus.Act(raycastCam);
            }

            else
            {
                cam.SetActive(false);
            }
        }
            
    }

    void Start()
    {
        if (CamerasinScene != null)
        {

            foreach (GameObject cam in CamerasinScene)
            {
                if (cam != CamerasinScene[0])
                {
                    cam.SetActive(false);
                }

                else
                {
                    int ActiveCamIndex = 0;
                    CameraChangeEvent ActiveCam = new CameraChangeEvent(ActiveCamIndex);
                    EventBus.Act(ActiveCam);

                    cam.SetActive(true);
                    InputChangeEvent raycastCam = new InputChangeEvent(cam);
                    EventBus.Act(raycastCam);
               }
            }

        }
    }

    // Update is called once per frame
    
}
