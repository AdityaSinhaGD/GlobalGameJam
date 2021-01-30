using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class UIManager : MonoBehaviour
{
    public Text wordText;
    public Text questionText;
    public Text answerText;
    public Text hudText;

    public Button confirmAnswerButton;

    [SerializeField] private GameObject keypadUI;
    public InputField inputField;

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
        keypadUI.SetActive(false);
    }

    private void Start()
    {
        confirmAnswerButton.onClick.AddListener(AnswerButtonPressed);
        inputField.onValueChanged.AddListener(ValidateInput);
        ResetHUDText();
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

    //private void InitializeKeyPadUIContainerObject()
    //{
    //    if (keypadUI != null)
    //    {
    //        InitializeDropdown();
    //    }
    //    else
    //    {
    //        Debug.Log("no Word dropdown active in scene");
    //    }

    //}

    //private void InitializeDropdown()
    //{
    //    wordCollectionDropdown = keypadUI.GetComponentInChildren<Dropdown>();
    //    foreach(string word in GameManager.Instance.wordsLearned)
    //    {
    //        UpdateWordCollectionDisplay(word);
    //    }
    //    wordCollectionDropdown.onValueChanged.AddListener(delegate
    //    {
    //        DropdownValueChanged(wordCollectionDropdown);
    //    });
    //}

    public void EnableKeyPadUI()
    {
        GameManager.Instance.PauseGame();
        keypadUI.SetActive(true);
    }

    public void DisableKeyPadUI()
    {
        keypadUI.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    //private void DropdownValueChanged(Dropdown change)
    //{
    //    int index = change.value;
    //    if (index != 0)
    //    {
    //        answerText.text = wordCollectionDropdown.options[index].text;
    //    }

    //}

    public void SetWordMessage(string message)
    {
        wordText.text = message + " learned";
    }

    public void ResetWordMessgage()
    {
        wordText.text = "";
    }

    private IEnumerator LearnWordRoutine(string word)
    {
        SetWordMessage(word);
        yield return new WaitForSeconds(2f);
        ResetWordMessgage();
    }

    public void DisplayLearnedWord(string word)
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
        hudText.text = message;
    }

    public void ResetHUDText()
    {
        hudText.text = "";
    }

    public void SetQuestionText(string message)
    {
        questionText.text = message;
    }
}
