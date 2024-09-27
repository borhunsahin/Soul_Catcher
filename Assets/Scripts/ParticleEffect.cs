using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    GameUIManager gameUIManager;
    AudioSource audioSource;
    void Start()
    {
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerDataManager.GetVolume();
        audioSource.mute = PlayerDataManager.GetSound();
    }
    void Update()
    {
        audioSource.volume = gameUIManager.gameSounds.volumeSlider.value;
        audioSource.mute = gameUIManager.gameSounds.soundToggle.isOn;
    }
}
