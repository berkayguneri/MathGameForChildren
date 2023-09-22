using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] int totalScore;
    [HideInInspector] int scoreArtis;

    [SerializeField] Text scoreText;

    private void Start()
    {
        scoreText.text = totalScore.ToString();
    }
    public void ScoreArttir(string zorlukSeviyesi)
    {
        switch (zorlukSeviyesi)
        {
            case "kolay":
                scoreArtis = 5;
                break;
            case "orta":
                scoreArtis =10;
                break;
            case "zor":
                scoreArtis = 15;
                break;
        }

        totalScore += scoreArtis;
        scoreText.text = totalScore.ToString();
    }


}
