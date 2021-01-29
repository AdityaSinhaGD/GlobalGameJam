using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour,IInteractable
{
    public string correctAnswer;
    public string question;

    private void Start()
    {
        UIManager.Instance.SetQuestionText(question);
        GameManager.Instance.correctWord = correctAnswer;
    }

    public void Interact()
    {
        UIManager.Instance.EnableKeyPadUI();
    }

    public void OnHover()
    {
        UIManager.Instance.SetHUDText("Keypad");
    }

    public void OnHoverExit()
    {
        UIManager.Instance.ResetHUDText();
    }
}
