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
    public GameObject enemyPrefab;
    public bool bossDead = false;
    //public GameObject sodaGrab;

    //----Private vars
    private int numberOfActiveCollectibles = 0;
    private GameObject player;
    private GameObject enemy;
    private int timing = 15;
    private int myScore = 0;
    private AudioSource calm;
    private bool playMusic = true;
    private int testScore = 0;

    void Start()
    {
        calm = GetComponent<AudioSource>();
        myScore = PlayerPrefs.GetInt("myScore", 0);
        player = Instantiate(playerPrefab);
        PlayerMove play = player.GetComponent<PlayerMove>();
        play.manager = this;
        //player.transform.position = Vector3.zero;
        if (player == null)
        {
            Debug.Log("Couldn't find player");
        }


        /*StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateSmallRock());*/

        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());

        StartCoroutine(EasyDifficulty());
        StartCoroutine(MedDifficulty());
        StartCoroutine(HardDifficulty());
        StartCoroutine(SpawnBoss());

        text.gameObject.SetActive(false);

        //StartCoroutine(theTime());
    }

    // Update is called once per frame
    void Update()
    {
        //game rules
        if (!calm.isPlaying && (playMusic = true))
        {
            calm.PlayOneShot(calm.clip, 0.2f);
        }
            
        if (bossDead)
        {
            PlayerPrefs.SetInt("myScore", gameScore);
            if (gameScore > PlayerPrefs.GetInt("highScore", 0))
            {
                PlayerPrefs.SetInt("highScore", gameScore);
            }
            Debug.Log("High Score");
            StartCoroutine(endGame());
        }

        //print Score every 100
        if (gameScore % 100 == 0 && gameScore != testScore)
        {
            testScore = gameScore;
            StartCoroutine(printScore());
        }

    }

    private IEnumerator printScore()
    {
        text.gameObject.SetActive(true);
        text.text = "Score: " + gameScore;
        yield return new WaitForSeconds(1);
        text.gameObject.SetActive(false);
    }
    public void incrementScore(int scoreValue)
    {
        gameScore += scoreValue;
        Debug.Log("Score: " + gameScore.ToString());
        //text.text = "Score: " + gameScore.ToString();
        numberOfActiveCollectibles--;

        //StartCoroutine(instantiateSmallRock());

    }

    IEnumerator instantiateSmallRock()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        int x = Random.Range(-15, 15);
        int y = Random.Range(-8, 18);
        int z = 100;

        GameObject instance = Instantiate(asteroid1);
        instance.transform.position = new Vector3(x, y, z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.manager = this;

        numberOfActiveCollectibles++;
        Debug.Log(x + "," + y + "," + z);
    }

    IEnumerator instantiateMediumRock()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        int x = Random.Range(-15, 15);
        int y = Random.Range(-8, 18);
        int z = 100;

        GameObject instance = Instantiate(asteroid2);
        instance.transform.position = new Vector3(x, y, z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.manager = this;

        numberOfActiveCollectibles++;
        Debug.Log(x + "," + y + "," + z);
    }

    IEnumerator instantiateLargeRock()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        int x = Random.Range(-15, 15);
        int y = Random.Range(-8, 18);
        int z = 100;

        GameObject instance = Instantiate(asteroid3);
        instance.transform.position = new Vector3(x, y, z);
        Asteroid collectable = instance.GetComponent<Asteroid>();
        collectable.manager = this;

        numberOfActiveCollectibles++;
        Debug.Log(x + "," + y + "," + z);
    }

    public void splitLargeRock(float firstX, float firstY, float firstZ)
    {

        Vector3 distance;

        distance = new Vector3(firstX, firstY, firstZ);

        GameObject instance = Instantiate(asteroid2);
        instance.transform.position = new Vector3(distance.x + 0.05f, distance.y + 0.01f, distance.z);
        Asteroid rock1 = instance.GetComponent<Asteroid>();
        rock1.direction = new Vector3(0.5f, 0, -1);
        rock1.speed -= 1;
        rock1.manager = this;
        Debug.Log("Splitting");

        GameObject instance2 = Instantiate(asteroid2);
        instance2.transform.position = new Vector3(distance.x - 0.05f, distance.y - 0.01f, distance.z);
        Asteroid rock2 = instance2.GetComponent<Asteroid>();
        rock2.direction = new Vector3(-0.5f, 0, -1);
        rock2.speed -= 1;
        rock2.manager = this;

    }

    public void splitMediumRock(float firstX, float firstY, float firstZ)
    {

        Vector3 distance;

        distance = new Vector3(firstX, firstY, firstZ);

        GameObject instance = Instantiate(asteroid1);
        instance.transform.position = new Vector3(distance.x + 0.05f, distance.y + 0.01f, distance.z);
        Asteroid rock1 = instance.GetComponent<Asteroid>();
        rock1.direction = new Vector3(0.5f, 0, -1);
        rock1.manager = this;
        rock1.speed -= 1;
        Debug.Log("Splitting");

        GameObject instance2 = Instantiate(asteroid1);
        instance2.transform.position = new Vector3(distance.x - 0.05f, distance.y - 0.01f, distance.z);
        Asteroid rock2 = instance2.GetComponent<Asteroid>();
        rock2.direction = new Vector3(-0.5f, 0, -1);
        rock2.speed -= 1;
        rock2.manager = this;

    }

    IEnumerator EasyDifficulty()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateSmallRock());
        Debug.Log("Starting Easy Section");
        yield return new WaitForSeconds(5);
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateMediumRock());
    }

    IEnumerator MedDifficulty()
    {
        yield return new WaitForSeconds(25);
        Debug.Log("Starting Medium Section");
        int generator = Random.Range(1, 5);
        StartCoroutine(instantiateSmallRock());
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateMediumRock());
        yield return new WaitForSeconds(5);
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());

        /*if (generator == 5)
            StartCoroutine(instantiateMediumRock());
        if(generator <= 4)
            StartCoroutine(instantiateSmallRock());*/
    }

    IEnumerator HardDifficulty()
    {
        yield return new WaitForSeconds(45);
        Debug.Log("Starting Hard Section");
        int generator = Random.Range(1, 20);
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateMediumRock());
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());
        yield return new WaitForSeconds(15);
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());
        StartCoroutine(instantiateLargeRock());

        for(int i = 0; i <= 1; i++)
        {
            if (generator >= 12)
                StartCoroutine(instantiateLargeRock());
            if (generator >= 5 && generator <= 11)
                StartCoroutine(instantiateMediumRock());
            if (generator <= 4)
                StartCoroutine(instantiateSmallRock());
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(80);
        playMusic = false;
        calm.Stop();
        yield return new WaitForSeconds(1);
        Debug.Log("Spawning Boss Fight");
        enemy = Instantiate(enemyPrefab);
        EnemyShipAI boss = enemy.GetComponent<EnemyShipAI>();
        boss.manager = this;
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGameScene");
    }
}
