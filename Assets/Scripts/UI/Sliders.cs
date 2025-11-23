using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sliders : MonoBehaviour
{
  public Slider Slider;
    float AudioSound;
    void Start()
    {
        AudioSound = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.onValueChanged.AddListener((v) => {

            Slider.value = v;


        });

    }

    public void VolumeAudio()
    {
        // AudioManager.instance.SFXVolumeAmount(Slider.value);
        AudioSound = Slider.value;
        SoundManager.Instance.VolumeAmount(AudioSound);

    }
}
