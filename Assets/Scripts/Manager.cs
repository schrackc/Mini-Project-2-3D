using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int gameScore = 0;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;

    public TMP_Text text;
    public GameObject playerPrefab;
    public TMP_Text timer;
    //public GameObject sodaGrab;

    //----Private vars
    private int numberOfActiveCollectibles = 0;
    private GameObject player;
    private int timing = 15;
    private int myScore = 0;
    private AudioSource calm;

    void Start()
    {
        //calm = GetComponent<AudioSource>();
        myScore = PlayerPrefs.GetInt("myScore", 0);
        player = Instantiate(playerPrefab);
        //player.transform.position = Vector3.zero;
        if (player == null)
        {
            Debug.Log("Couldn't find player");
        }


        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());

        //text.text = "Score: 0";
        //timer.text = "Time: 15";

        //StartCoroutine(theTime());
    }

    // Update is called once per frame
    void Update()
    {
        //game rules
        /*if (!calm.isPlaying)
            calm.PlayOneShot(calm.clip, 0.2f);*/
        if (timing <= 0)
        {
            PlayerPrefs.SetInt("myScore", gameScore);
            if (gameScore > PlayerPrefs.GetInt("highScore", 0))
            {
                PlayerPrefs.SetInt("highScore", gameScore);
            }
            Debug.Log("High Score");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }

    }
    public void incrementScore(int scoreValue)
    {
        gameScore += scoreValue;
        Debug.Log("Score: " + gameScore.ToString());
        //text.text = "Score: " + gameScore.ToString();
        numberOfActiveCollectibles--;

        StartCoroutine(instantiateSmallRock());

    }

    IEnumerator theTime()
    {
        while (timing > 0)
        {
            yield return new WaitForSeconds(1);
            timing--;
            timer.text = "Time: " + timing;
        }
    }

    IEnumerator instantiateSmallRock()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        int x = Random.Range(-150, 150);
        int y = Random.Range(-80, 80);
        int z = 100;

        GameObject instance = Instantiate(asteroid1);
        instance.transform.position = new Vector3(x, y, z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.manager = this;

        numberOfActiveCollectibles++;
        Debug.Log(x + "," + y + "," + z);
    }
}
