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
        //InitializeCrosshair();
        wordsLearned.Add("GameJam");
    }

    //private void InitializeCrosshair()
    //{
    //    crosshair = GameObject.Find("Crosshair");
    //    Cursor.lockState = CursorLockMode.Locked; //Lock cursor
    //    Cursor.visible = false; //Hide Cursor
    //    if (crosshair != null)
    //        crosshair.SetActive(true); //Show Crosshair
    //}

    // Update is called once per frame
    void Update()
    {
        ProcessPauseCommand();
    }

    private void ProcessPauseCommand()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
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
        }
        
    }

    public void PauseGame()
    {
        if (UIManager.Instance)
        {
            state = GameState.paused;
            isPaused = true;
            UIManager.Instance.DisableCrosshair();
        }
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
