using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour,IInteractable
{

    public void Interact()
    {
        UIManager.Instance.EnableKeyPadUI();
    }

    public void OnHover()
    {
        
    }

    public void OnHoverExit()
    {
        
    }
}
