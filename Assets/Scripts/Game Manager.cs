using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float score;
    public float highScore;
    

    public Canvas endGameCanvas;

    AudioSource _point;
    public bool audioButton = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        Application.targetFrameRate = 120;

        _point = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetFloat("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        Scoring();
        PlayerPrefs.Save();

        if(audioButton)
        {
            _point.Play();
            audioButton = false;
        }
    }

    void Scoring()
    {
        if(highScore <= score)
        {
            PlayerPrefs.SetFloat("highScore", score);
            highScore = score;
        }
    }

    public void Replay()
    {
        score = 0;
    }
}
