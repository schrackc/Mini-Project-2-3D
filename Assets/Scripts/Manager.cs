using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int gameScore = 0;
    public GameObject smallAsteroid;
    //public GameObject medAsteroid;

    public TMP_Text text;
    public GameObject playerPrefab;
    public TMP_Text timer;
    //public GameObject sodaGrab;

    //----Private vars
    private int numberOfActiveCollectibles = 0;
    private GameObject player;
    private int timing = 15;
    public int myScore = 0;
    public int highScore = 0;
    private AudioSource calm;

    void Start()
    {
        //calm = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt("highScore", 0);
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
            if (gameScore > highScore)
            {
                PlayerPrefs.SetInt("highScore", gameScore);
            }
            Debug.Log("High Score");
            timer.text = "GAME OVER!!";
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
        Vector3 distance;
        int x;
        int y;
        int z;
        do
        {
            x = Random.Range(-10, 10);
            y = Random.Range(2, 8);
            z = Random.Range(10, 20);
            distance = new Vector3(x - player.transform.position.x, y, z - player.transform.position.z);

        } while (distance.magnitude < 2);

        //if the object passes the border
        if (distance.x >= 11 || distance.x <= -11 || distance.z >= 21 || distance.z <= 9)
        {
            x = Random.Range(-8, 8);
            z = Random.Range(-3, 3);
            distance = new Vector3(x, 0, z);
        }

        GameObject instance = Instantiate(smallAsteroid);
        instance.transform.position = new Vector3(distance.x, distance.y, distance.z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.manager = this;

        numberOfActiveCollectibles++;
        Debug.Log(distance.x + "," + distance.y + "," + distance.z);
    }
}
