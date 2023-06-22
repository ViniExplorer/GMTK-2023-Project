using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    public Slider slider;
    public enum Type
    {
        Music,
        SFX
    }

    Type type;
    private void Start()
    {
        if(type == Type.Music)
        {
            slider.value = PlayerPrefs.GetFloat("Music");
        }
        else if (type == Type.SFX)
        {
            slider.value = PlayerPrefs.GetFloat("hi");
        }
    }

    private void Update()
    {
        if(type == Type.Music)
        {
            PlayerPrefs.SetFloat("Music", slider.value);
        }
        else if (type == Type.SFX)
        {
            PlayerPrefs.SetFloat("SFX", slider.value);
        }
    }
}
