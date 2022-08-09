using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] track;
    public AudioSource sauce;

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().buildIndex < 4 && sauce.clip != track[0])
        {
            sauce.clip = track[0];
            sauce.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 4 && sauce.clip != track[1])
        {
            sauce.clip = track[1];
            sauce.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex > 4 && sauce.clip != track[2])
        {
            sauce.clip = track[2];
            sauce.Play();
        }
    }
}
