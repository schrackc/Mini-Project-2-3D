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
    private float glideSpeed = 5;
    private AudioSource bossMusic;
    private Vector3 direction;
    private Vector3 travelLocation;
    // Start is called before the first frame update
    void Start()
    {
        bossMusic = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 150, 150);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossMusic.isPlaying)
            bossMusic.PlayOneShot(bossMusic.clip, 0.5f);
        //AI Fighting
        if (introMoveEnd)
        {
            direction = travelLocation;
            Vector3.ClampMagnitude(direction, 2);
            direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
            transform.position += glideSpeed * Time.deltaTime * direction;
        }
        //AI Intro movement down
        else
        {
            introMovement();
            if (transform.position.y <= 0)
            {
                introMoveEnd = true;
                travelLocation = manager.waypoint1.transform.position;
            }
        }

        //Game ends player dies
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

        //Change waypoint
        if (gameObject.tag == "Waypoint")
        {
            int waypointChoice = Random.Range(1, 3);
            //Set new waypoint
            if (gameObject.name == "Waypoint1")
            {
                if (waypointChoice == 1) travelLocation = manager.waypoint2.transform.position;
                if (waypointChoice == 2) travelLocation = manager.waypoint3.transform.position;
                if (waypointChoice == 3) travelLocation = manager.waypoint4.transform.position;
            }
            if (gameObject.name == "Waypoint2")
            {
                if (waypointChoice == 1) travelLocation = manager.waypoint1.transform.position;
                if (waypointChoice == 2) travelLocation = manager.waypoint3.transform.position;
                if (waypointChoice == 3) travelLocation = manager.waypoint4.transform.position;
            }
            if (gameObject.name == "Waypoint3")
            {
                if (waypointChoice == 1) travelLocation = manager.waypoint1.transform.position;
                if (waypointChoice == 2) travelLocation = manager.waypoint2.transform.position;
                if (waypointChoice == 3) travelLocation = manager.waypoint4.transform.position;
            }
            if (gameObject.name == "Waypoint4")
            {
                if (waypointChoice == 1) travelLocation = manager.waypoint1.transform.position;
                if (waypointChoice == 2) travelLocation = manager.waypoint2.transform.position;
                if (waypointChoice == 3) travelLocation = manager.waypoint3.transform.position;
            }
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
