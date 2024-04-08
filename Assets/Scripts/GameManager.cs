using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject levelFinishedScreen;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource levelMusic;
    public void GameOver()
    {

        levelMusic.Stop();
        gameOverSound.Play();
        
        gameoverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LevelFinished()
    {
        levelFinishedScreen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
