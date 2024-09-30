﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    GameManager gameManager;

    public GamePanels gamePanels;

    AudioSource audioSource;
    public GameSounds gameSounds;

    InterstitialAdController interstitialAdController;
    RewardedInterstitialAdController rewardedInterstitialAdController;
    RewardedAdController rewardedAdController;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        interstitialAdController = GameObject.Find("InterstitialAdManager").GetComponent<InterstitialAdController>();

        gameSounds.volumeSlider.value = PlayerDataManager.GetVolume();
        gameSounds.soundToggle.isOn = PlayerDataManager.GetSound();
        gameSounds.musicToggle.isOn = PlayerDataManager.GetMusic();
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        gamePanels.pausePanel.SetActive(true);
        gamePanels.pauseText.SetActive(false);
        gamePanels.unPauseText.SetActive(true);
        audioSource.PlayOneShot(gameSounds.buttonSound);
        
    }
    public void ContinueButton()
    {
        Time.timeScale = 1;
        gamePanels.pausePanel.SetActive(false);
        gamePanels.pauseText.SetActive(true);
        gamePanels.unPauseText.SetActive(false);
        audioSource.PlayOneShot(gameSounds.buttonSound);
    }
    public void SettingsButton()
    {
        gamePanels.settingsPanel.SetActive(true);
        audioSource.PlayOneShot(gameSounds.buttonSound);
    }
    public void BackButton()
    {
        gamePanels.settingsPanel.SetActive(false);
        audioSource.PlayOneShot(gameSounds.buttonSound);
    }
    public void MainMenuButton()
    {
        audioSource.PlayOneShot(gameSounds.buttonSound);
        StartCoroutine(LoadAsync(0));
    }
    public void ExitButton()
    {
        gamePanels.exitQuaryPanel.SetActive(true);
        audioSource.PlayOneShot(gameSounds.exitButtonSound);
    }
    public void StayButton()
    {
        gamePanels.exitQuaryPanel.SetActive(false);
        audioSource.PlayOneShot(gameSounds.buttonSound);
    }
    public void LeaveButton()
    {
        audioSource.PlayOneShot(gameSounds.exitButtonSound);
        Application.Quit();
    }
    public void RestartButton()
    {
        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex));
    }
    public void RestartButtonwithAds()
    {
        gamePanels.restartButtonwithAds.SetActive(false);
        gamePanels.restartButtonwithAdsPause.SetActive(false);
        gamePanels.restartButtonPause.SetActive(true);
        interstitialAdController.ShowAd();
    }
    public void NextLevelButton()
    {
        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void VolumeSlider()
    {
        audioSource.volume = gameSounds.volumeSlider.value;
        gameSounds.gameMusic.volume = gameSounds.volumeSlider.value;
        PlayerDataManager.SetVolume(gameSounds.volumeSlider.value);
    }
    public void SoundToggle()
    {
        audioSource.mute = gameSounds.soundToggle.isOn;
        gameSounds.gameMusic.mute = gameSounds.soundToggle.isOn;
        gameSounds.musicToggle.isOn = gameSounds.soundToggle.isOn;
        PlayerDataManager.SetSound(gameSounds.soundToggle.isOn);
        PlayerDataManager.SetMusic(gameSounds.musicToggle.isOn);
    }
    public void MusicToggle()
    {
        if (!audioSource.mute)
        {
            gameSounds.gameMusic.mute = gameSounds.musicToggle.isOn;
            PlayerDataManager.SetMusic(gameSounds.musicToggle.isOn);
        }
    }

    public void EndGameProcess(bool isGameOver) // İsmini düzelt
    {
        gamePanels.endLevelPanel.SetActive(true);
        if(gameManager.isGameOver)
        {
            gamePanels.nextLevelText.SetActive(false);
            gamePanels.gameOverText.SetActive(true);
            gamePanels.nextLevelButton.SetActive(false);
        }
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation aSyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        gamePanels.loadingPanel.SetActive(true);

        while (!aSyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(aSyncOperation.progress / 0.9f);
            gamePanels.loadingSlider.value = progress;
            yield return null;
        }
    }
}
[Serializable]
public struct GamePanels
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject exitQuaryPanel;
    public GameObject endLevelPanel;

    public GameObject pauseText;
    public GameObject unPauseText;
    public GameObject gameOverText;
    public GameObject nextLevelText;

    public GameObject nextLevelButton;

    public GameObject restartButtonwithAds;

    public GameObject restartButtonwithAdsPause;
    public GameObject restartButtonPause;

    public GameObject loadingPanel;
    public Slider loadingSlider;
}
    
[Serializable]
public struct GameSounds
{
    public Slider volumeSlider;
    public Toggle soundToggle;
    public Toggle musicToggle;

    public AudioClip buttonSound;
    public AudioClip exitButtonSound;

    public AudioSource gameMusic;
}
