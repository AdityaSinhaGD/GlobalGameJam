using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private Button firstLevelSelectButton;
    [SerializeField] private Button secondLevelSelectButton;
    [SerializeField] private Button thirdLevelSelectButton;
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        levelSelectPanel.SetActive(false);
        levelSelectButton.onClick.AddListener(() => { levelSelectPanel.SetActive(true); });
        firstLevelSelectButton.onClick.AddListener(() => { GameManager.Instance.LoadGameLevel(1); });
        secondLevelSelectButton.onClick.AddListener(() => { GameManager.Instance.LoadGameLevel(2); });
        thirdLevelSelectButton.onClick.AddListener(() => { GameManager.Instance.LoadGameLevel(3); });
        backButton.onClick.AddListener(() => { levelSelectPanel.SetActive(false); });
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadGameScene();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
