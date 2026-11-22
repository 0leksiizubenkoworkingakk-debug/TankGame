using System;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private Button ReloadButton;

    void Start()
    {
        ReloadButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        ReloadButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameLoose()
    {
        TitleText.text = "Ви програли Почати гру знову";
    }

    public void GameWin()
    {
        TitleText.text = "Ви перемогли Почати новий рывень";
    }
}