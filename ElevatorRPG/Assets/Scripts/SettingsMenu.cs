using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }


}
