using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    public GameObject gameOverPanel;
    public Animator gameOverAnim;
    public Button playAgainBtn, backBtn;
    public Text finalScore;

    private void Awake()
    {
        MakeInstance();
        InitializeView();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void InitializeView()
    {
        finalScore.text = "Score : ";
        gameOverPanel.SetActive(false);

    }

    public void PlayAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void BackToMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void GameOverShowPanel()
    {
        if (ScoreManager.instance != null)
        {
            finalScore.text ="Score \n"+ ScoreManager.instance.GetScore();
            ScoreManager.instance.scoreText.gameObject.SetActive(false);
        }
        gameOverPanel.SetActive(true);
        gameOverAnim.Play("FadeIn");
    }
}