using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManagerr : MonoBehaviour
{
    [SerializeField] GameObject startButton, exitButton;
    void Start()
    {
        FadeOut();
    }


    void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void StartButton()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
