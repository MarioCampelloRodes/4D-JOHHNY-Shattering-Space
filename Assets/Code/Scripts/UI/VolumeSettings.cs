using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Credits")
        {
            if (PlayerPrefs.HasKey("AudioVolume"))
            {
                if (PlayerPrefs.GetInt("AudioVolume") == 0)
                    volumeSlider.value = 0f;
                else if (PlayerPrefs.GetInt("AudioVolume") == 1)
                    volumeSlider.value = 0.25f;
                else if (PlayerPrefs.GetInt("AudioVolume") == 2)
                    volumeSlider.value = 0.5f;
                else if (PlayerPrefs.GetInt("AudioVolume") == 3)
                    volumeSlider.value = 1f;

                AudioListener.volume = volumeSlider.value;
            }
            else
            {
                PlayerPrefs.SetInt("AudioVolume", 3);
                volumeSlider.value = 1f;
                AudioListener.volume = volumeSlider.value;
            }
        }
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;

        if(sliderValue >= 0 && sliderValue < 0.17f)
            PlayerPrefs.SetInt("AudioVolume", 0);
        else if(sliderValue >= 0.17 && sliderValue < 0.5f)
            PlayerPrefs.SetInt("AudioVolume", 1);
        else if (sliderValue >= 0.5 && sliderValue < 0.83f)
            PlayerPrefs.SetInt("AudioVolume", 2);
        else if (sliderValue > 0.83f)
            PlayerPrefs.SetInt("AudioVolume", 3);

        AudioListener.volume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
