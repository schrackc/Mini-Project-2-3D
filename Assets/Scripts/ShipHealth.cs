using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public GameObject greenHealth;
    public GameObject redHealth;
    public float health = 20;
    private float startHealth;
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
        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGameScene");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Enemy" || gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            health--;
            greenHealth.transform.localScale = new Vector3(health / startHealth, 0.05f, 0.001f);
            StartCoroutine(tempShow());
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
}
