using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;
    public float spinSpeed;
    public int pointValue = 5;
    public Manager manager;

    private Vector3 direction;
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, -1);
        rotation = new Vector3(spinSpeed, spinSpeed, spinSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = speed * Time.deltaTime * direction;
        transform.position += speed * Time.deltaTime * direction;

        //Rotate Astroid
        transform.Rotate(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            manager.incrementScore(pointValue);
            Destroy(this.gameObject);
        }
        if (gameObject.tag == "Player")
        {
            //Need Player Damage with Player Health
            Destroy(this.gameObject);
        }
    }
}
