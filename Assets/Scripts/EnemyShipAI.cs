using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    public float speed;
    public float health = 20;
    public Manager manager;

    private bool introMoveEnd = false;
    private float glideSpeed = 10;
    private AudioSource bossMusic;
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

        }
        //AI Intro movement down
        else
        {
            introMovement();
            if (transform.position.y <= 0)
                introMoveEnd = true;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    private void introMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0);
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
        transform.position += glideSpeed * Time.deltaTime * direction;
    }
}
