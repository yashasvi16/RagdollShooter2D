using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text scoreUI;
    public TMP_Text highScoreUI;
    public TMP_Text finalScoreUI;
    public Canvas endGameCanvas;
    public bool playerDead;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = "Kill Count: " + GameManager.Instance.score;
        highScoreUI.text = "Highest Kills: " + GameManager.Instance.highScore;

        if (playerDead)
        {
            EndGamePanel();
        }
    }
    void EndGamePanel()
    {
        endGameCanvas.gameObject.SetActive(true);
        finalScoreUI.text = "Final Kill Count: " + GameManager.Instance.score;
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
