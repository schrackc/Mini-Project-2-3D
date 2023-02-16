using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    public int pointValue = 5;
    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = speed * Time.deltaTime * direction;
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Bullet")
        {
            manager.incrementScore(pointValue);
            Destroy(this.gameObject);
        }
    }
}
