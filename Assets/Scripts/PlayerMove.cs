using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float lerpConstant;

    private Vector3 direction;
    


    void Start()
    {
        direction = Vector3.zero;
        
    }

    private Vector3 GetInput()
    {
        Vector3 dir = Vector3.zero;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (transform.position.y < 15))
        {
            //dir.z = -1;
            dir += transform.forward;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (transform.position.y > -10))
        {
            //dir.z = 1;
            dir -= transform.forward;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (transform.position.x < 30))
        {
            //dir.x = -1;
            dir += transform.right;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (transform.position.x > -30))
        {
            //dir.x = 1;
            dir -= transform.right;
        }
        
        return dir.normalized;
    }

    void Update()
    {
        Vector3 inputVelocity = GetInput();
        direction += inputVelocity;
        Vector3.ClampMagnitude(direction, 2);
        direction = Vector3.Lerp(direction, Vector2.zero, lerpConstant);
        transform.position += speed * Time.deltaTime * direction;
        
    }
}
