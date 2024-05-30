using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource backgroundMusic; //기본
    public AudioSource dungeonMusic; //던전
    public AudioSource bossMusic; //보스전

    private void Start()
    {
        backgroundMusic.Play();
        dungeonMusic.Stop();
        bossMusic.Stop();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToBossMusic();
        }
    }

    private void SwitchToBackgroundMusic()
    {
        backgroundMusic.Play();
        dungeonMusic.Stop();
        bossMusic.Stop();
    }

    private void SwitchDungeonMusic()
    {
        backgroundMusic.Stop();
        dungeonMusic.Play();
        bossMusic.Stop();
    }

    private void SwitchToBossMusic()
    {
        backgroundMusic.Stop();
        dungeonMusic.Stop();
        bossMusic.Play();
    }
}