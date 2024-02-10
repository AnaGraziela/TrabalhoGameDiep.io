using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public bool statusSound = true;
    public AudioSource backgroundSound;
    public Sprite soundActivated;
    public Sprite soundDesactivated;
    public Image image;
    public void ActiveOrDesactiveSound()
    {
        statusSound = !statusSound;
        backgroundSound.enabled = statusSound;

        if (statusSound)
        {
            image.sprite = soundActivated;
        }
        else
        {
            image.sprite = soundDesactivated;
        }
    }
    public void MusicSound(float value)
    {
        backgroundSound.volume = value;
    }
}
