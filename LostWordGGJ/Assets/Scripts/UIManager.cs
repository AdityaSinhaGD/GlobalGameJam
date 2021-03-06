﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI lettersLearnedText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI objectText;
    public TextMeshProUGUI answerFeedBackText;

    public Button confirmAnswerButton;
    public Button closeKeypadUIButton;

    [SerializeField] private GameObject keypadUI;
    public TMP_InputField inputField;

    public GameObject crosshair;

    public GameObject pauseMenu;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;

    public GameObject gameOverMenu;

    public bool isInteractionOngoing = false;

    public float timeLimit = 60f;
    private float currentTime;
    public TextMeshProUGUI timerText;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        keypadUI.SetActive(false);
        InitializeCrosshair();
    }

    private void Start()
    {
        GameManager.Instance.ResumeGame();
        currentTime = timeLimit;
        InitializeListeners();
        ResetHUDText();
    }

    private void Update()
    {
        if(GameManager.Instance.state == GameManager.GameState.running || GameManager.Instance.state == GameManager.GameState.paused)
        {
            if(currentTime <= 0)
            {
                GameManager.Instance.EndGame();
            }
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("0");
        }
    }

    private void InitializeListeners()
    {
        confirmAnswerButton.onClick.AddListener(AnswerButtonPressed);
        inputField.onValueChanged.AddListener(ValidateInput);
        resumeButton.onClick.AddListener(() => { GameManager.Instance.ResumeGame(); });
        mainMenuButton.onClick.AddListener(() => { GameManager.Instance.LoadMainMenu(); });
        quitButton.onClick.AddListener(() => { GameManager.Instance.QuitGame(); });
        closeKeypadUIButton.onClick.AddListener(() => { DisableKeyPadUI(); });
        ResetWordMessgage();
        ResetAnswerFeedbackText();
    }

    private void InitializeCrosshair()
    {
        crosshair = GameObject.Find("Crosshair");
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false; //Hide Cursor
        if (crosshair != null)
            crosshair.SetActive(true); //Show Crosshair
    }

    public void EnableCrosshair()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false; //Hide Cursor
        if (crosshair != null)
            crosshair.SetActive(true); //Show Crosshair 
    }

    public void DisableCrosshair()
    {
        Cursor.lockState = CursorLockMode.None; //Allow user to move cursor
        Cursor.visible = true; //Show Cursor
        if (crosshair != null) //Hide Crosshair
            crosshair.SetActive(false);
    }

    private void ValidateInput(string arg0)
    {
        string allowedCharacters = new string(GameManager.Instance.lettersLearned.ToArray());
        string allowedCharactersLowerCase = allowedCharacters.ToLower();
        string allowedCharactersUpperCase = allowedCharacters.ToUpper();

        var match = Regex.IsMatch(inputField.text, @"^[xX" + allowedCharactersLowerCase + allowedCharactersUpperCase + "]+$");
        if (!match)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                return;
            }
            else
            {
                inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
            }
        }
    }


    public void EnableKeyPadUI()
    {
        isInteractionOngoing = true;
        GameManager.Instance.PauseGame();
        keypadUI.SetActive(true);
    }

    public void DisableKeyPadUI()
    {
        isInteractionOngoing = false;
        keypadUI.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void SetWordMessage(string message)
    {
        lettersLearnedText.text = message;
    }

    public void ResetWordMessgage()
    {
        lettersLearnedText.text = "";
    }

    private IEnumerator LearnWordRoutine(string word)
    {
        SetWordMessage(word);
        yield return new WaitForSeconds(2f);
        ResetWordMessgage();
    }

    public void SetWordText(string word)
    {
        StartCoroutine(LearnWordRoutine(word));
    }

    //public void UpdateWordCollectionDisplay(string word)
    //{
    //    wordCollectionDropdown.options.Add(new Dropdown.OptionData() { text = word });
    //}

    public void AnswerButtonPressed()
    {
        if(inputField.text.ToLower() == GameManager.Instance.correctWord.ToLower())
        {
            StartCoroutine(FeedbackRoutine(true));
        }
        else
        {
            StartCoroutine(FeedbackRoutine(false));
        }
    }

    private IEnumerator FeedbackRoutine(bool isAnswerCorrect)
    {
        if (isAnswerCorrect)
        {
            SetAnswerFeedbackText("Nightmare slain");
            DisableKeyPadUI();
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            SetAnswerFeedbackText("The nightmare continues");
            yield return new WaitForSeconds(2f);
            ResetAnswerFeedbackText();
        }
        
    }

    public void SetHUDText(string message)
    {
        objectText.text = message;
    }

    public void ResetHUDText()
    {
        objectText.text = "";
    }

    public void SetQuestionText(string message)
    {
        questionText.text = message;
    }

    public void SetAnswerFeedbackText(string message)
    {
        answerFeedBackText.text = message;
    }

    public void ResetAnswerFeedbackText()
    {
        answerFeedBackText.text = "";
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}
