using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public GameObject greenHealth;
    public GameObject redHealth;
    public ParticleSystem bigExplosion;
    public ParticleSystem smallExplosion;
    public float health = 20;
    private float startHealth;
    private bool playerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        greenHealth.SetActive(false);
        redHealth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !playerDead)
        {
            StartCoroutine(death());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Enemy" || gameObject.tag == "Bullet")
        {
            smallExplosion = Instantiate(smallExplosion);
            smallExplosion.transform.position = gameObject.transform.position;
            smallExplosion.Play();
            Destroy(gameObject);
            health--;
            greenHealth.transform.localScale = new Vector3(health / startHealth, 0.05f, 0.001f);
            StartCoroutine(tempShow());
            Debug.Log("Health is: " + health);
        }
    }

    private IEnumerator tempShow()
    {

        greenHealth.SetActive(true);
        redHealth.SetActive(true); 
        yield return new WaitForSeconds(1.5f);
        greenHealth.SetActive(false);
        redHealth.SetActive(false);
    }

    private IEnumerator death()
    {
        playerDead = true;
        bigExplosion = Instantiate(bigExplosion);
        bigExplosion.transform.position = transform.position;
        bigExplosion.Play();
        transform.position = new Vector3(0,0,-100);
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGameScene");
    }
}
