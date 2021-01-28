using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject crosshair;

    public enum GameState { running, paused, over }
    public GameState state = GameState.running;
    private bool isPaused = false;

    public override void Awake()
    {
        base.Awake();
        InitializeCrosshair();
    }

    private void InitializeCrosshair()
    {
        crosshair = GameObject.Find("crosshair");
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false; //Hide Cursor
        if (crosshair != null)
            crosshair.SetActive(true); //Show Crosshair
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPauseCommand();
    }

    private void ProcessPauseCommand()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && state != GameState.paused) //Pauses the game
        {
            state = GameState.paused;
            isPaused = !isPaused;
            Cursor.lockState = CursorLockMode.None; //Allow user to move cursor
            Cursor.visible = true; //Show Cursor
            if (crosshair != null) //Hide Crosshair
                crosshair.SetActive(false);

        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && state == GameState.paused)
        {
            state = GameState.running;
            Cursor.lockState = CursorLockMode.Locked; //Lock cursor
            Cursor.visible = false; //Hide Cursor
            if (crosshair != null)
                crosshair.SetActive(true); //Show Crosshair 
        }
    }
}
