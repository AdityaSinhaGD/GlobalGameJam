using System;
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

    public Button confirmAnswerButton;
    public Button closeKeypadUIButton;

    [SerializeField] private GameObject keypadUI;
    public TMP_InputField inputField;

    public GameObject crosshair;
    public GameObject pauseMenu;
    public Button resumeButton;

    public bool isInteractionOngoing = false;

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
        pauseMenu.SetActive(false);
        keypadUI.SetActive(false);
        InitializeCrosshair();
    }

    private void Start()
    {
        InitializeListeners();
        ResetHUDText();
    }

    private void InitializeListeners()
    {
        confirmAnswerButton.onClick.AddListener(AnswerButtonPressed);
        inputField.onValueChanged.AddListener(ValidateInput);
        resumeButton.onClick.AddListener(() => { GameManager.Instance.ResumeGame(); });
        closeKeypadUIButton.onClick.AddListener(() => { DisableKeyPadUI(); });
        ResetWordMessgage();
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

        var match = Regex.IsMatch(inputField.text, @"^[x" + allowedCharacters + "]+$");
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
            Debug.Log("You Defeated");
        }
        else
        {
            Debug.Log("Wrong Answer Try again");
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
}
