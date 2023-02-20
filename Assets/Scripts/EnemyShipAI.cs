using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    public float speed;
    public float health = 20;
    public Manager manager;
    //public GameObject baseShip;

    private bool introMoveEnd = false;
    private float glideSpeed = 10;
    private AudioSource bossMusic;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        bossMusic = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 150, 125);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossMusic.isPlaying)
            bossMusic.PlayOneShot(bossMusic.clip, 0.5f);
        //AI Fighting
        if (introMoveEnd)
        {
            transform.position += glideSpeed * Time.deltaTime * direction;
        }
        //AI Intro movement down
        else
        {
            introMovement();
            if (transform.position.y <= 0)
            {
                introMoveEnd = true;
                direction = chooseMovement();
            }
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
        if (PlayerPrefs.GetString("Progress", "Stay") == "Finish")
        {
            //Play Explosion Particles
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            health--;
            manager.bossDead = true;
        }
    }

    private void introMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0);
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
        transform.position += glideSpeed * Time.deltaTime * direction;
    }

    private void Movement(int x, int y)
    {
        Vector3 direction = new Vector3(x, y, 0);
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, direction, 0.2f);
        //this.transform.position = direction;
        transform.position += glideSpeed * Time.deltaTime * direction;
    }

    private Vector3 chooseMovement()
    {
        int x = Random.Range(1, -1);
        int y = Random.Range(1, -1);
        Vector3 direction = new Vector3(x, y, 0);
        Vector3.ClampMagnitude(direction, 2);
        return direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
        //this.transform.position = direction;
        //transform.position += glideSpeed * Time.deltaTime * direction;
    }
}
