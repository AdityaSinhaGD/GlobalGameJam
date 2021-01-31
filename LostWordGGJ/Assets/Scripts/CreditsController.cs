using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; //Allow user to move cursor
        Cursor.visible = true; //Show Cursor
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
