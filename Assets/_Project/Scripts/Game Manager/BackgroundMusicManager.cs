using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;

    public void StopBackgroundMusic()
    {
        _backgroundMusic.Stop();
    }
}
