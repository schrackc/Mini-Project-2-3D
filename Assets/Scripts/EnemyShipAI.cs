using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    public float speed;
    public float health = 40;
    public Manager manager;
    //public GameObject baseShip;

    private bool introMoveEnd = false;
    private float glideSpeed = 7;
    private AudioSource bossMusic;
    private Vector3 direction;
    private Vector3 travelLocation;
    private GameObject waypoint;
    // Start is called before the first frame update
    void Start()
    {
        bossMusic = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 150, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossMusic.isPlaying)
            bossMusic.PlayOneShot(bossMusic.clip, 0.5f);
        //AI Fighting
        if (introMoveEnd)
        {
            travelLocation = waypoint.transform.position - transform.position;
            travelLocation.z = 0;
            travelLocation = travelLocation.normalized;
            direction += travelLocation;
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
                waypoint = manager.waypoint1;
            }
        }

        //Game ends player dies
        if (health <= 0)
        {
            manager.bossDead = true;
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
        }

        //Change waypoint
        if (gameObject.tag == "Waypoint")
        {
            int waypointChoice = Random.Range(1, 3);
            //Set new waypoint
            if (gameObject.name == "Waypoint1")
            {
                if (waypointChoice == 1) waypoint = manager.waypoint2;
                if (waypointChoice == 2) waypoint = manager.waypoint3;
                if (waypointChoice == 3) waypoint = manager.waypoint4;
            }
            if (gameObject.name == "Waypoint2")
            {
                if (waypointChoice == 1) waypoint = manager.waypoint1;
                if (waypointChoice == 2) waypoint = manager.waypoint3;
                if (waypointChoice == 3) waypoint = manager.waypoint4;
            }
            if (gameObject.name == "Waypoint3")
            {
                if (waypointChoice == 1) waypoint = manager.waypoint1;
                if (waypointChoice == 2) waypoint = manager.waypoint2;
                if (waypointChoice == 3) waypoint = manager.waypoint4;
            }
            if (gameObject.name == "Waypoint4")
            {
                if (waypointChoice == 1) waypoint = manager.waypoint1;
                if (waypointChoice == 2) waypoint = manager.waypoint2;
                if (waypointChoice == 3) waypoint = manager.waypoint3;
            }
            direction = new Vector3(0,0,0);
        }
    }

    private void introMovement()
    {
        Vector3 direction = new Vector3(0, -1, 0);
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, Vector2.zero, 0.2f);
        transform.position += 25 * Time.deltaTime * direction;
    }
}
