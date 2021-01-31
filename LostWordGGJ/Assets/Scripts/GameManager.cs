using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //public GameObject crosshair;

    public enum GameState { running, paused, over }
    public GameState state = GameState.running;
    private bool isPaused = false;

    public List<char> lettersLearned = new List<char>();
    public List<string> wordsLearned = new List<string>();
    public string correctWord;
    
    public override void Awake()
    {
        base.Awake();
        wordsLearned.Add("GameJam");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPauseCommand();
    }

    private void ProcessPauseCommand()
    {
        if (state==GameState.running||state==GameState.paused)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    if (!UIManager.Instance.isInteractionOngoing)
                    {
                        ResumeGame();
                    }

                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        if (UIManager.Instance)
        {
            state = GameState.running;
            isPaused = false;
            UIManager.Instance.EnableCrosshair();
            UIManager.Instance.pauseMenu.SetActive(false);
        }
        
    }

    public void PauseGame()
    {
        if (UIManager.Instance)
        {
            state = GameState.paused;
            isPaused = true;
            UIManager.Instance.DisableCrosshair();
            if (!UIManager.Instance.isInteractionOngoing)
            {
                UIManager.Instance.pauseMenu.SetActive(true);
            }
            
        }
        
    }

    public void EndGame()
    {
        if (UIManager.Instance)
        {
            state = GameState.over;
            UIManager.Instance.gameOverMenu.SetActive(true);
            UIManager.Instance.DisableCrosshair();
            UIManager.Instance.pauseMenu.SetActive(false);
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
