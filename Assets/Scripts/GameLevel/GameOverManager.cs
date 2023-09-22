using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene("MenuLevel");
    }
}
