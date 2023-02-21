using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMove : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 0.1f;

    
    // Start is called before the first frame update
    void Start()
    {
        

        //direction = new Vector2(1, 0);
        //Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {

        //Move();
        
        Vector3 t = speed * Time.deltaTime * direction;
        transform.position += speed * Time.deltaTime * direction;

        if (this.transform.position.z <= -30)
            Destroy(this.gameObject);
    }

    private void Move()
    {
        //Vector3 newPosition = new Vector3(speed * transform.position.x * Time.deltaTime, speed * transform.position.y * Time.deltaTime, 0);
        Vector3 newPosition = new Vector3(direction.x, direction.y, speed * transform.position.z * Time.deltaTime);
        this.transform.position -= newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            Destroy(this.gameObject);
        }
        if (gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Destroy(this.gameObject);
        }
    }
}
