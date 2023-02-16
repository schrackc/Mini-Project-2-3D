using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    

    private Vector3 direction;
    


    void Start()
    {
        direction = Vector3.zero;
        
    }

    private Vector3 GetInput()
    {
        Vector3 dir = Vector3.zero;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (transform.position.y < 10))
        {
            //dir.z = -1;
            dir += transform.up;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (transform.position.y > 0))
        {
            //dir.z = 1;
            dir -= transform.up;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (transform.position.x < 11))
        {
            //dir.x = -1;
            dir += transform.right;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x > -11))
        {
            //dir.x = 1;
            dir -= transform.right;
        }
        
        return dir.normalized;
    }

    void Update()
    {
        direction = GetInput();
        Vector3 t = speed * Time.deltaTime * direction;
        transform.position += speed * Time.deltaTime * direction;
        
    }
}
