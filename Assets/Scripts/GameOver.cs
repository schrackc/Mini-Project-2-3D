using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public Manager manager;
    public TMP_Text endGame;
    public TMP_Text finalScore;
    public TMP_Text yourScore;
    private int bestScore = 0;
    private int hisScore = 0;
    //public ButtonEditor button;
    private AudioSource easy;

    // Start is called before the first frame update
    void Start()
    {
        easy = GetComponent<AudioSource>();
        endGame.text = "Game Over!!";
        hisScore = PlayerPrefs.GetInt("myScore", 0);
        bestScore = PlayerPrefs.GetInt("highScore", 0);

        Debug.Log("Best Score: " + bestScore);
        finalScore.text = ("High Score: " + bestScore);
        yourScore.text = ("Your Score: " + hisScore);
        //PlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        if (!easy.isPlaying)
            easy.PlayOneShot(easy.clip, 0.5f);
        if (Input.GetKey(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }
}
